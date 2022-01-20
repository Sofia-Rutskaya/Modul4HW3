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
    public class EmployeeProjectConfigurations : IEntityTypeConfiguration<EmployeeProject>
    {
        public void Configure(EntityTypeBuilder<EmployeeProject> builder)
        {
            builder.ToTable("EmployeeProject").HasKey(p => p.EmployeeProjectId);
            builder.Property(p => p.EmployeeProjectId).ValueGeneratedOnAdd();
            builder.Property(p => p.Rate).HasColumnType("money");
            builder.Property(p => p.StartedDate).IsRequired().HasMaxLength(27).HasColumnType("datetime");
            builder.Property(p => p.EmployeeId).IsRequired();
            builder.Property(p => p.ProjectId).IsRequired();

            builder.HasOne(d => d.Employee)
                .WithMany(p => p.EmployeeProjects)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(d => d.Project)
                .WithMany(p => p.EmployeeProjects)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
