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
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasKey(r => r.Id);

            builder.HasOne(r => r.Tourist)
                   .WithMany(u => u.WrittenReviews)
                   .HasForeignKey(r => r.TouristId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(r => r.TourGuide)
                   .WithMany(u => u.ReceivedReviews)
                   .HasForeignKey(r => r.TourGuideId)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
