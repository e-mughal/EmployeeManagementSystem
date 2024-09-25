using EmployeeManagementAPI.Core.Interface;
using EmployeeManagementAPI.Infrastructure.Data.Repository;
using Microsoft.EntityFrameworkCore;
using PracticeAPI.Core.Interface;
using PracticeAPI.Infrastructure.Data;
using PracticeAPI.Infrastructure.Data.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<EmployeeContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.

builder.Services.AddControllers();


// using the scoped lifetime for the services.
// creates a new object for a user's request. 
builder.Services.AddScoped<IEmployee, EmployeesRepository>();
builder.Services.AddScoped<IDepartment, DepartmentsRepository>();
builder.Services.AddScoped<IAuth, AuthRepository>();
builder.Services.AddScoped<IUser, UserRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
