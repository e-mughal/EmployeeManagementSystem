using EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.Application.Interface
{
    public interface IUser
    {
        Task LogInAsync(User user);
        Task LogOutAsync(User user);
        Task ClearUserAsync();
        //Task<string> GetCurrentUserStringAsync();
        Task<User> GetCurrentUserAsync();
    }
}
