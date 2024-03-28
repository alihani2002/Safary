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
        }
    }
}
