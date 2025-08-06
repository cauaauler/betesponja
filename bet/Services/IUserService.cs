using FutebolSimplesBetsHub.Models;

namespace FutebolSimplesBetsHub.Services
{
    public interface IUserService
    {
        Task<User?> GetUserByIdAsync(int id);
        Task<User?> GetUserByUsernameAsync(string username);
        Task<User?> GetUserByEmailAsync(string email);
        Task<User> CreateUserAsync(string name, string email, string username, string password);
        Task<bool> ValidateUserAsync(string username, string password);
        Task<bool> UpdateUserAsync(User user);
    }
} 