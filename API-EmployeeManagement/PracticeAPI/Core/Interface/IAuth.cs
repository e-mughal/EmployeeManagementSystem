using EmployeeManagementAPI.Core.Entities;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementAPI.Core.Interface
{
    public interface IAuth
    {
        Task<Auth> GetUserById(int id);
        Task RegisterAsync(Auth newUser);
        Task<bool> LoginAsync([FromBody] LoginRequest request);
        Task<Auth> GetNameAsync([FromBody] LoginRequest request);
        Task<Auth> GetUserDetailsAsync(User user);
        Task<List<Auth>> GetAllUsersAsync();
        Task UpdateUserAsync(Auth user);
        Task DeleteUserAsync(int id);
    }
}
