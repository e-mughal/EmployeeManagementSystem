using EmployeeManagementAPI.Core.Entities;
using EmployeeManagementAPI.Core.Interface;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticeAPI.Infrastructure.Data;

namespace EmployeeManagementAPI.Infrastructure.Data.Repository
{
    public class AuthRepository : IAuth
    {
        private readonly EmployeeContext _context;

        public AuthRepository(EmployeeContext context)
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
        public async Task<bool> LoginAsync([FromBody] LoginRequest request)
        {
            var success = _context.Auth.Any(e => e.Email == request.Email && e.Password == request.Password);
            if(success)
            {
                return true;
            }
            return false;
        }
        public async Task<Auth> GetNameAsync([FromBody] LoginRequest request)
        {
            var auth = await _context.Auth
                .Where(e => e.Email == request.Email && e.Password == request.Password)
                .FirstOrDefaultAsync();

            if (auth != null)
            {
                return auth;
            }

            return null;
        }
        public async Task<Auth> GetUserDetailsAsync(User user)
        {
            var authUser = await _context.Auth
                .Where(e => e.Name == user.Name && e.Email == user.Email)
                .FirstOrDefaultAsync();

            if(authUser != null)
            {  
                return authUser; 
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
            var auth = await _context.Auth.FindAsync(id);
            if(auth != null)
            {
                _context.Auth.Remove(auth);
                await _context.SaveChangesAsync();
            }
        }
    }
}
