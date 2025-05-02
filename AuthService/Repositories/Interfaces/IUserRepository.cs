using AuthService.Models;
using SharedKernel.Interfaces;

namespace AuthService.Repositories.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
    }
}
