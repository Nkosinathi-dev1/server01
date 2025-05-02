using AuthService.Data;
using AuthService.Models;
using AuthService.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Repositories;


namespace AuthService.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly AuthDbContext _context;
        public UserRepository(AuthDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
