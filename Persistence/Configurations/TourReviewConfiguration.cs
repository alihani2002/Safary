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
    public class TourReviewConfiguration : IEntityTypeConfiguration<TourReview>
    {
        public void Configure(EntityTypeBuilder<TourReview> builder)
        {
            builder.HasOne(r => r.Tour)
                .WithMany(t => t.Reviews)
                .HasForeignKey(r => r.TourName);

            builder.HasOne(r => r.Tourist)
                .WithMany(u => u.TourReviews)
                .HasForeignKey(r => r.TouristId);
        }
    }

}
