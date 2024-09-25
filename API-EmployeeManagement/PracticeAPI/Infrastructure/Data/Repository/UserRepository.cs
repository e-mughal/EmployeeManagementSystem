using EmployeeManagementAPI.Core.Entities;
using EmployeeManagementAPI.Core.Interface;
using Microsoft.EntityFrameworkCore;
using PracticeAPI.Infrastructure.Data;

namespace EmployeeManagementAPI.Infrastructure.Data.Repository
{
    public class UserRepository : IUser
    {
        private readonly EmployeeContext _context;
        public UserRepository(EmployeeContext context)
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
            var users = await _context.User.ToListAsync();
            _context.User.RemoveRange(users);
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
            return await _context.User.FirstOrDefaultAsync();
        }
    }
}
