using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagementAPI.Core.Entities;
using Microsoft.EntityFrameworkCore;
using PracticeAPI.Core.Entities;

namespace PracticeAPI.Infrastructure.Data
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options) 
            : base(options)
        {
        }

        public DbSet<Employee> Employee { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Auth> Auth { get; set; } = default!;
        public DbSet<User> User { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).HasColumnName("Name");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("Departments");
                entity.HasKey(e => e.DeptId);
                entity.Property(e => e.DeptName).HasColumnName("DeptName");
            });

            modelBuilder.Entity<Auth>(entity =>
            {
                entity.ToTable("Auth");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).HasColumnName("Name");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");
                entity.HasKey(e => e.Name);
                entity.Property(e => e.Name).HasColumnName("Name");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
