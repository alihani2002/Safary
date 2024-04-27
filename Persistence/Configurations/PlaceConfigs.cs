using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configurations
{
    public class PlaceConfigs : IEntityTypeConfiguration<Place>
    {
        public void Configure(EntityTypeBuilder<Place> builder)
        {

            builder.HasOne(a => a.City)
                   .WithMany(g => g.Places)
                   .HasForeignKey(a => a.CityId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(a => a.TourDay)
                   .WithMany(g => g.Places)
                   .HasForeignKey(a => a.TourDayId)
                   .OnDelete(DeleteBehavior.SetNull);
            
            builder.HasOne(a => a.TourHour)
                   .WithMany(g => g.Places)
                   .HasForeignKey(a => a.TourHourId)
                   .OnDelete(DeleteBehavior.SetNull);


        }
    }
}
