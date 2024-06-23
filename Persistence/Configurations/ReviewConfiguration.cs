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
    public class ReviewConfiguration : IEntityTypeConfiguration<TourGuideReview>
    {
        public void Configure(EntityTypeBuilder<TourGuideReview> builder)
        {
           
        }
    }
}
