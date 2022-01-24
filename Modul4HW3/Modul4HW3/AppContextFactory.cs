using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modul4HW3.Data;
using Modul4HW3.Data.Entity;
using Modul4HW3.Data.EntityConfigurations;
using Microsoft.EntityFrameworkCore.Design;

namespace Modul4HW3
{
    public class AppContextFactory : IDesignTimeDbContextFactory<ApplicationsDbContext>
    {
        ApplicationsDbContext IDesignTimeDbContextFactory<ApplicationsDbContext>.CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationsDbContext>();

            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();

            var connectionString = config.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString, opts => opts.CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds));
            return new ApplicationsDbContext(optionsBuilder.Options);
        }
    }
}
