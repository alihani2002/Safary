using Domain.Consts;
using Domain.Filters;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
	public class TourHour : BaseModel
    {
        public int Id { get; set; }
        [MaxLength(150, ErrorMessage = Errors.MaxLength)]
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public string Location { get; set; } = null!;
        [Range(1, 23, ErrorMessage = Errors.MaxHourDuration)]
        public int Duration { get; set; }
        public string ImageUrl { get; set; } = null!;
        public DateTime StartDate { get; set; }
        [DateGreaterThan("StartDate")]
        public DateTime EndDate { get; set; }
        public ICollection<Place>? Places { get; set; }
        public int? PersonId { get; set; }
        public ApplicationUser? Person { get; set; }
        public int? CityId { get; set; }
        public City? City { get; set; }

    }
}
