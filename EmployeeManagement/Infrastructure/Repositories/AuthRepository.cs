using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Interface;
using EmployeeManagement.Infrastructure.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Infrastructure.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly EmployeeManagementContext _context;

        public AuthRepository(EmployeeManagementContext context)
        {
            _context = context;
        }

        public async Task<Auth> GetUserById(int id)
        {
            return await _context.Auth.FindAsync(id);
        }

        public async Task RegisterAsync(Auth newUser)
        {
            await _context.Auth.AddAsync(newUser);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            var success = _context.Auth.Any(e => e.Email == username && e.Password == password);
            if (success)
            {
                return true;
            }
            return false;
        }

        public async Task<string?> GetNameAsync(string username, string password)
        {
            var auth = await _context.Auth
                .Where(e => e.Email == username && e.Password == password)
                .Select(e => e.Name)
                .FirstOrDefaultAsync();

            if(auth != null)
            {
                return auth;
            }

            return null;
        }

        public async Task<Auth> GetUserDetailsAsync(User user)
        {
            var auth = await _context.Auth
                .Where(e => e.Name == user.Name && e.Email == user.Email)
                .FirstOrDefaultAsync();

            if(auth != null)
            {
                return auth;
            }
            return null;
        }

        public async Task<List<Auth>> GetAllUsersAsync()
        {
            return await _context.Auth.ToListAsync();
        }

        public async Task UpdateUserAsync(Auth user)
        {
            _context.Auth.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _context.Auth.FindAsync(id);
            if(user != null)
            {
                _context.Auth.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}
