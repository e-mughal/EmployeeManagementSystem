using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using EmployeeManagement.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc.Razor;
using EmployeeManagement.Infrastructure.Repositories;
using EmployeeManagement.Domain.Interface;
using EmployeeManagement.Application.Service;
using EmployeeManagement.Application.Interface;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<EmployeeManagementContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("EmployeeManagementContext") ?? throw new InvalidOperationException("Connection string 'EmployeeManagementContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.Configure<RazorViewEngineOptions>(options =>
{
    options.ViewLocationFormats.Clear();
    options.ViewLocationFormats.Add("/Presentation/Views/{1}/{0}.cshtml");
    options.ViewLocationFormats.Add("/Presentation/Views/Shared/{0}.cshtml");
});

// Need to add link to the local api for testing
builder.Services.AddHttpClient("API", client =>
{
    client.BaseAddress = new Uri("ADD API LOCALHOST:PORT LINK");
});

builder.Services.AddScoped<IEmployee, EmployeeService>(sp =>
    new EmployeeService(sp.GetRequiredService<IHttpClientFactory>().CreateClient("API")));

builder.Services.AddScoped<IDepartment, DepartmentService>(sp =>
    new DepartmentService(sp.GetRequiredService<IHttpClientFactory>().CreateClient("API")));

builder.Services.AddScoped<IAuth, AuthService>(sp =>
    new AuthService(sp.GetRequiredService<IHttpClientFactory>().CreateClient("API")));

builder.Services.AddScoped<IUser, UserService>(sp =>
    new UserService(sp.GetRequiredService<IHttpClientFactory>().CreateClient("API")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
