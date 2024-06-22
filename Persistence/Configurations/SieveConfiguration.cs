
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Sieve.Models;
using Sieve.Services;
namespace Persistence.Configurations
{
    public class SieveConfiguration : ISieveConfiguration
    {
        public void Configure(SievePropertyMapper mapper)
        {
            // Blog
            mapper.Property<Blog>(p => p.Title)
                  .CanFilter()
                  .CanSort();
            mapper.Property<Blog>(p => p.Description)
                  .CanFilter()
                  .CanSort();
            mapper.Property<Blog>(p => p.Content)
                  .CanFilter()
                  .CanSort();

            // Tour
            mapper.Property<Tour>(p => p.Name)
                  .CanFilter()
                  .CanSort();
            mapper.Property<Tour>(p => p.Description)
                  .CanFilter()
                  .CanSort();

            // Tour Hour 
            mapper.Property<TourHour>(p => p.Name)
                 .CanFilter()
                 .CanSort();
            mapper.Property<TourHour>(p => p.Location)
                 .CanFilter()
                 .CanSort();

            // Tour Guide
            mapper.Property < ApplicationUser>(p => p.HourPrice)
                .CanFilter()
                .CanSort();
            mapper.Property<ApplicationUser>(p => p.Rate)
                .CanFilter()
                .CanSort();


        }
    }

}
