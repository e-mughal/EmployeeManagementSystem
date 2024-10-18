using EmployeeManagement.Application.Interface;
using EmployeeManagement.Domain.Entities;
using System.Net.Http.Json;

namespace EmployeeManagement.Application.Service
{
    public class EmployeeService : IEmployee
    {
        private readonly HttpClient _httpClient;

        public EmployeeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/employee/id/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Employee>();
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            var response = await _httpClient.GetAsync("api/employee");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<Employee>>();
        }

        public async Task<List<Employee>> GetAllEmployeesListAsync()
        {
            var response = await _httpClient.GetAsync("api/employee/list");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Employee>>();
        }

        public async Task<IEnumerable<Employee>> GetEmployeesByDeptAsync(string department)
        {
            var response = await _httpClient.GetAsync($"api/employee/department/{department}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<Employee>>();
        }

        public async Task<IEnumerable<Employee>> EmployeeSearchAsync(string searchName)
        {
            var response = await _httpClient.GetAsync($"api/employee/{searchName}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<Employee>>();
        }

        public async Task AddEmployeeAsync(Employee employee)
        {
            var response = await _httpClient.PostAsJsonAsync("api/employee", employee);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/employee/{employee.Id}", employee);
            response.EnsureSuccessStatusCode();
        }
        
        public async Task DeleteEmployeeAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/employee/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<bool> EmployeeExistsAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/employee/exist/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<bool>();
        }

    }
}
