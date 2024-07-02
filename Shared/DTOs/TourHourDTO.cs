using Domain.Consts;
using Domain.Entities;
using Domain.Filters;
using Sieve.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    public class TourHourDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Location { get; set; } = null!;
        public int Duration { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<TourImageDTO>? TourImages { get; set; }
        public double AverageRating { get; set; }

    }
    public class TourImageDTO
    {
        public string ImageUrl { get; set; } = null!;
    }
}
