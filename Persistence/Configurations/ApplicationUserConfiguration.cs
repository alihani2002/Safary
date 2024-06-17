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
   
        public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
        {
            public void Configure(EntityTypeBuilder<ApplicationUser> builder)
            {
                builder.HasMany(u => u.WrittenReviews)
                       .WithOne(r => r.Tourist)
                       .HasForeignKey(r => r.TouristId);

                builder.HasMany(u => u.ReceivedReviews)
                       .WithOne(r => r.TourGuide)
                       .HasForeignKey(r => r.TourGuideId);
            }
        }
}
