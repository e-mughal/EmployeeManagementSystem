using EmployeeManagement.Application.Interface;
using EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.Application.Service
{
    public class DepartmentService : IDepartment
    {
        private readonly HttpClient _httpClient;

        public DepartmentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Department>> GetAllDepartmentsAsync()
        {
            var response = await _httpClient.GetAsync("api/department");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<Department>>();
        }

        public async Task AddDepartmentAsync(Employee employee)
        {
            var response = await _httpClient.PostAsJsonAsync("api/department", employee);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateDepartmentAsync(Employee employee)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/department/{employee.Id}", employee);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteDepartmentAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/department/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
