using EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.Application.Interface
{
    public interface IAuth
    {
        Task<Auth> GetUserById(int id);
        Task RegisterAsync(Auth newUser);
        Task<bool> LoginAsync(string username, string password);
        Task<Auth> GetNameAsync(string username, string password);
        Task<Auth> GetUserDetailsAsync(User user);
        Task<List<Auth>> GetAllUsersAsync();
        Task UpdateUserAsync(Auth user);
        Task DeleteUserAsync(int id);
    }
}
