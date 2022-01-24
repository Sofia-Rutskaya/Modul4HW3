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
    public class ClientConfigurations : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Client").HasKey(p => p.ClientId);
            builder.Property(p => p.ClientId).ValueGeneratedOnAdd();
            builder.Property(p => p.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(p => p.LastName).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Email).IsRequired().HasMaxLength(27);
            builder.Property(p => p.Phone).IsRequired();

            builder.HasData(new List<Client>()
             {
                new Client { ClientId = 1, FirstName = "Ellie", LastName = "Scottish", Email = "oafja[of@gmail.com", Phone = 01384920 },
                new Client { ClientId = 2, FirstName = "Matt", LastName = "Brutt", Email = "sreyw@gmail.com", Phone = 37424724 },
                new Client { ClientId = 3, FirstName = "Oliver", LastName = "Holland", Email = "orywf@gmail.com", Phone = 35864657 },
                new Client { ClientId = 4, FirstName = "Hadderson", LastName = "Mallyis", Email = "okrdtf@gmail.com", Phone = 35737372 },
                new Client { ClientId = 5, FirstName = "Olya", LastName = "Ametya", Email = "dtktex@gmail.com", Phone = 374876479 },
                new Client { ClientId = 6, FirstName = "Yacno", LastName = "Ugosgs", Email = "srjuwyy@gmail.com", Phone = 323575 }
             });
        }
    }
}
