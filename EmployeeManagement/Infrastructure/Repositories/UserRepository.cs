using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Interface;
using EmployeeManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly EmployeeManagementContext _context;

        public UserRepository(EmployeeManagementContext context)
        {
            _context = context;
        }

        public async Task LogInAsync(User user)
        {
            await _context.User.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task LogOutAsync(User user)
        {
            _context.User.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task ClearUserAsync()
        {
            var allUsers = await _context.User.ToListAsync();

            _context.User.RemoveRange(allUsers);
            await _context.SaveChangesAsync();
        }

        public async Task<string> GetCurrentUserStringAsync()
        {
            var user = await _context.User.FirstOrDefaultAsync();
            if(user == null)
            {
                return null;
            }
            return user.Name;
        }

        public async Task<User> GetCurrentUserAsync()
        {
            var user = await _context.User.FirstOrDefaultAsync();
            return user;
        }
    }
}
