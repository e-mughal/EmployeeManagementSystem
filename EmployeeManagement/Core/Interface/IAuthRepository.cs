using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Interface;
using EmployeeManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Domain.Interface
{
    public interface IAuthRepository
    {
        Task<Auth> GetUserById(int id);
        Task RegisterAsync(Auth newUser);
        Task<bool> LoginAsync(string username, string password);
        Task<string> GetNameAsync(string username, string password);
        Task<Auth> GetUserDetailsAsync(User user);
        Task<List<Auth>> GetAllUsersAsync();
        Task UpdateUserAsync(Auth user);
        Task DeleteUserAsync(int id);
    }
}
