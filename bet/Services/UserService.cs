using Microsoft.EntityFrameworkCore;
using FutebolSimplesBetsHub.Data;
using FutebolSimplesBetsHub.Models;

namespace FutebolSimplesBetsHub.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> CreateUserAsync(string name, string email, string username, string password)
        {
            // Check if user already exists
            if (await GetUserByUsernameAsync(username) != null)
                throw new InvalidOperationException("Nome de usuário já existe");

            if (await GetUserByEmailAsync(email) != null)
                throw new InvalidOperationException("Email já existe");

            var user = new User
            {
                Name = name,
                Email = email,
                Username = username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
                Balance = 1000.00m,
                Role = "user",
                IsActive = true,
                CreatedAt = DateTime.Now
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<bool> ValidateUserAsync(string username, string password)
        {
            var user = await GetUserByUsernameAsync(username);
            if (user == null || !user.IsActive)
                return false;

            return BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            try
            {
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
} 