using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Emit;

namespace Persistence.Configurations
{
    public class BlogConfigs : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            //builder.HasMany(a => a.TourDays)
            //   .WithOne(s => s.Blog)
            //   .OnDelete(DeleteBehavior.SetNull);

            //builder.HasMany(a => a.TourHours)
            //   .WithOne(s => s.Blog)
            //   .OnDelete(DeleteBehavior.SetNull);

        }
    }
}
