using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modul4HW3.Data.Entity;

namespace Modul4HW3.Data.EntityConfigurations
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("Project").HasKey(p => p.ProjectId);
            builder.Property(p => p.ProjectId).HasColumnName("ProjectId").ValueGeneratedOnAdd();
            builder.Property(p => p.Name).HasColumnName("Name").IsRequired().HasMaxLength(50);
            builder.Property(p => p.Budget).IsRequired().HasColumnName("Budget").HasColumnType("money");
            builder.Property(p => p.StartedDate).IsRequired().HasMaxLength(27);

            builder.HasOne(d => d.Clients)
               .WithMany(p => p.ClientProjects)
               .HasForeignKey(d => d.ClientId)
               .OnDelete(DeleteBehavior.Cascade);

            // builder.HasData(new List<Project>()
            // {
            //    new Project { ProjectId = 1, Name = "Project1", Budget = 2314, StartedDate = new DateTime(1998, 09, 04) },
            //    new Project { ProjectId = 2, Name = "Project2", Budget = 1365135, StartedDate = new DateTime(2011, 10, 04) },
            //    new Project { ProjectId = 3, Name = "Project3", Budget = 23515, StartedDate = new DateTime(2018, 12, 09) },
            //    new Project { ProjectId = 4, Name = "Project4", Budget = 135316, StartedDate = new DateTime(2021, 06, 12) },
            //    new Project { ProjectId = 5, Name = "Project5", Budget = 1363153, StartedDate = new DateTime(2019, 04, 15) }
            // });
        }
    }
}
