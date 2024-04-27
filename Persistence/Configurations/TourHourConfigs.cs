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
    public class TourHourConfigs : IEntityTypeConfiguration<TourHour>
    {
        public void Configure(EntityTypeBuilder<TourHour> builder)
        {
            builder.HasOne(a => a.Blog)
                   .WithMany(g => g.TourHours)
                   .HasForeignKey(a => a.BlogId)
                   .OnDelete(DeleteBehavior.SetNull);
            builder.HasMany(a => a.Places)
              .WithOne(s => s.TourHour)
              .OnDelete(DeleteBehavior.SetNull);

        }
    }
}
