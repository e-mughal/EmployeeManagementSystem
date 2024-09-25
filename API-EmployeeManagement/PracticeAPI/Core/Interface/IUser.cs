using EmployeeManagementAPI.Core.Entities;

namespace EmployeeManagementAPI.Core.Interface
{
    public interface IUser
    {
        Task LogInAsync(User user);
        Task LogOutAsync(User user);
        Task ClearUserAsync();
        Task<string> GetCurrentUserStringAsync();
        Task<User> GetCurrentUserAsync();
    }
}
