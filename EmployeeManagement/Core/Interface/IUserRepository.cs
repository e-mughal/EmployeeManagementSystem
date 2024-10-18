using EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.Domain.Interface
{
    public interface IUserRepository
    {
        Task LogInAsync(User user);
        Task LogOutAsync(User user);
        Task ClearUserAsync();
        Task<string> GetCurrentUserStringAsync();
        Task<User> GetCurrentUserAsync();
    }
}
