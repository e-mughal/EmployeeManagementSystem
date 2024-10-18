using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.Infrastructure.Data
{
    public class EmployeeManagementContext : DbContext
    {
        public EmployeeManagementContext(DbContextOptions<EmployeeManagementContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employee { get; set; } = default!;
        public DbSet<Department> Departments { get; set; } = default!;
        public DbSet<Auth> Auth { get; set; } = default!;
        public DbSet<User> User { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>()
                .HasKey(d => d.DeptId);
            modelBuilder.Entity<Auth>()
                .HasKey(e => e.Id);
            modelBuilder.Entity<User>()
                .HasKey(e => e.Name);
        }



        //public DbSet<EmployeeManagement.Models.Department> Department { get; set; }
    }
}
