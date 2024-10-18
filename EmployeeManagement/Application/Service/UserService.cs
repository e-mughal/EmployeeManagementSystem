using EmployeeManagement.Application.Interface;
using EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.Application.Service
{
    public class UserService : IUser
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        //public async Task<string> GetCurrentUserStringAsync()
        //{
        //    var response = await _httpClient.GetAsync("api/user/currentstring");
        //    response.EnsureSuccessStatusCode();

        //    return await response.Content.ReadFromJsonAsync<string>();
        //}

        public async Task<User> GetCurrentUserAsync()
        {
            var response = await _httpClient.GetAsync("api/user/current");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<User>();
        }

        public async Task LogInAsync(User user)
        {
            var response = await _httpClient.PostAsJsonAsync("api/user/login", user);
            response.EnsureSuccessStatusCode();
        }

        public async Task LogOutAsync(User user)
        {
            var response = await _httpClient.PostAsJsonAsync("api/user/logout", user);
            response.EnsureSuccessStatusCode();
        }

        public async Task ClearUserAsync()
        {
            var response = await _httpClient.PostAsync("api/user/clear", null);
            response.EnsureSuccessStatusCode();
        }

    }
}
