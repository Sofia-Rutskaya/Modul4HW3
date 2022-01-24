using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Modul4HW3.Data.Entity;
using Modul4HW3.Data.EntityConfigurations;

namespace Modul4HW3.Data
{
    public class ApplicationsDbContext : DbContext
    {
        public ApplicationsDbContext(DbContextOptions<ApplicationsDbContext> context)
            : base(context)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Title> Titles { get; set; }
        public DbSet<Office> Offices { get; set; }
        public DbSet<EmployeeProject> EmployeeProjects { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Client> Clients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProjectConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            modelBuilder.ApplyConfiguration(new OfficeConfiguration());
            modelBuilder.ApplyConfiguration(new TitleConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeProjectConfigurations());
            modelBuilder.ApplyConfiguration(new ClientConfigurations());
        }
    }
}
