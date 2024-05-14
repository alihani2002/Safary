using Domain.Consts;
using Domain.Entities;
using Domain.Filters;
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
        public string ImageUrl { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ICollection<Place>? Places { get; set; }
        //public int? TourGuideId { get; set; }
        //public ApplicationUser? TourGuide { get; set; }
  
    }
}
