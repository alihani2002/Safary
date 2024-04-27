using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configurations
{
    public class CityConfigs : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasMany(a => a.Places)
               .WithOne(s => s.City)
               .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(a => a.Country)
                   .WithMany(g => g.Cities)
                   .HasForeignKey(a => a.CountryId)
                   .OnDelete(DeleteBehavior.SetNull);

        }
    }
}
