using AuthService.Dtos;

namespace AuthService.Services.Interfaces
{
    public interface IAuthService
    {
        Task<TokenResponseDto> RegisterAsync(RegisterDto dto);
        Task<TokenResponseDto> LoginAsync(LoginDto dto);
        Task<TokenResponseDto> RefreshTokenAsync(string refreshToken);
    }
}
