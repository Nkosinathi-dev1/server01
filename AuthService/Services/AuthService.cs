using AuthService.Data;
using AuthService.Dtos;
using AuthService.Helpers;
using AuthService.Models;
using AuthService.Repositories.Interfaces;
using AuthService.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly AuthDbContext _context;

        public AuthService(IUserRepository userRepository, ITokenService tokenService, AuthDbContext context)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _context = context;
        }

        public async Task<TokenResponseDto> RegisterAsync(RegisterDto dto)
        {
            if (await _userRepository.GetByEmailAsync(dto.Email) != null)
                throw new Exception("User already exists.");

            PasswordHasher.CreatePasswordHash(dto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new User
            {
                Email = dto.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            await _userRepository.AddAsync(user);
            await _userRepository.SaveAsync();

            var accessToken = _tokenService.GenerateAccessToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken();

            var refreshTokenEntity = new RefreshToken
            {
                Token = refreshToken,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                UserId = user.Id
            };

            await _context.RefreshTokens.AddAsync(refreshTokenEntity);
            await _context.SaveChangesAsync();

            return new TokenResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpiresAt = DateTime.UtcNow.AddMinutes(15)
            };
        }

        public async Task<TokenResponseDto> LoginAsync(LoginDto dto)
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email);
            if (user == null || !PasswordHasher.VerifyPassword(dto.Password, user.PasswordHash, user.PasswordSalt))
                throw new Exception("Invalid credentials.");

            var accessToken = _tokenService.GenerateAccessToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken();

            var refreshTokenEntity = new RefreshToken
            {
                Token = refreshToken,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                UserId = user.Id
            };

            await _context.RefreshTokens.AddAsync(refreshTokenEntity);
            await _context.SaveChangesAsync();

            return new TokenResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpiresAt = DateTime.UtcNow.AddMinutes(15)
            };
        }

        public async Task<TokenResponseDto> RefreshTokenAsync(string refreshToken)
        {
            var tokenEntity = await _context.RefreshTokens
               .Include(rt => rt.User)
               .FirstOrDefaultAsync(rt => rt.Token == refreshToken && !rt.IsRevoked);

            if (tokenEntity == null || tokenEntity.ExpiresAt < DateTime.UtcNow)
                throw new Exception("Invalid refresh token.");

            tokenEntity.IsRevoked = true;

            var newAccessToken = _tokenService.GenerateAccessToken(tokenEntity.User);
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            var newRefreshTokenEntity = new RefreshToken
            {
                Token = newRefreshToken,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                UserId = tokenEntity.UserId
            };

            await _context.RefreshTokens.AddAsync(newRefreshTokenEntity);
            await _context.SaveChangesAsync();

            return new TokenResponseDto
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
                ExpiresAt = DateTime.UtcNow.AddMinutes(15)
            };
        }


    }
}
