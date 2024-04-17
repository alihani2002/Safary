using Domain.Consts;
using Domain.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TourHour : BaseModel
    {
        public int Id { get; set; }
        [MaxLength(150, ErrorMessage = Errors.MaxLength)]
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Location { get; set; } = null!;
        [Range(1, 23, ErrorMessage = Errors.MaxHourDuration)]
        public int Duration { get; set; }
        public string ImageUrl { get; set; } = null!;
        public DateTime StartDate { get; set; }
        [DateGreaterThan("StartDate")]
        public DateTime EndDate { get; set; }
        public ICollection<Place>? Places { get; set; }
        public int? TourGuideId { get; set; }
        public TourGuide? TourGuide { get; set; }
        public int? CountryId { get; set; }
        public Country? Country { get; set; }
        public int? TouristId { get; set; }
        public Tourist? Tourist { get; set; }
        public int BlogId { get; set; }
        public Blog? Blog { get; set; }

    }
}
