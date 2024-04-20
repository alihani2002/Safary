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
           builder.HasKey(x => x.Id);
           builder.HasMany(b => b.TourDays)
           .WithOne()
           .HasForeignKey(td => td.Id);

            builder.HasMany(b => b.TourHours)
           .WithOne()
           .HasForeignKey(td => td.Id);
        }
    }
}
