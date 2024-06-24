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
    public class TourGuideReviewConfiguration : IEntityTypeConfiguration<TourGuideReview>
    {
        public void Configure(EntityTypeBuilder<TourGuideReview> builder)
        {
            builder.HasOne(r => r.TourGuide)
                .WithMany(u => u.TourGuideReviews)
                .HasForeignKey(r => r.TourGuideId);

            builder.HasOne(r => r.Tourist)
                .WithMany(u => u.TourGuideTouristReviews)
                .HasForeignKey(r => r.TouristId); // Specify ON DELETE NO ACTION here
        }
    }
}