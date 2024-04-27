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
    public class TourDayConfigs : IEntityTypeConfiguration<TourDay>
    {
        public void Configure(EntityTypeBuilder<TourDay> builder)
        {
            builder.HasOne(a => a.Blog)
                   .WithMany(g => g.TourDays)
                   .HasForeignKey(a => a.BlogId)
                   .OnDelete(DeleteBehavior.SetNull);


            builder.HasMany(a => a.Places)
               .WithOne(s => s.TourDay)
               .OnDelete(DeleteBehavior.SetNull);

        }
    }
}
