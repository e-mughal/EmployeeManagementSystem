using EmployeeManagement.Application.Interface;
using EmployeeManagement.Domain.Entities;
using Microsoft.AspNetCore.Identity.Data;
using System.Net.Http;

namespace EmployeeManagement.Application.Service
{
    public class AuthService : IAuth
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Auth> GetUserById(int id)
        {
            var response = await _httpClient.GetAsync($"api/auth/id/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Auth>();
        }
        public async Task<List<Auth>> GetAllUsersAsync()
        {
            var response = await _httpClient.GetAsync("api/auth");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Auth>>();
        }
        public async Task<bool> LoginAsync(string username, string password)
        {
            var credentials = new LoginRequest { Email = username, Password = password };
            var response = await _httpClient.PostAsJsonAsync("api/auth/login", credentials);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<bool>();
        }
        public async Task<Auth> GetNameAsync(string username, string password)
        {
            var credentials = new LoginRequest { Email = username, Password = password };
            var response = await _httpClient.PostAsJsonAsync("api/auth/getName", credentials);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Auth>();
        }
        public async Task RegisterAsync(Auth newUser)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth", newUser);
            response.EnsureSuccessStatusCode();
        }
        public async Task<Auth> GetUserDetailsAsync(User user)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/getdetails", user);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Auth>();
        }
        public async Task UpdateUserAsync(Auth user)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/auth/{user.Id}", user);
            response.EnsureSuccessStatusCode();
        }
        public async Task DeleteUserAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/auth/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
