using Domain.Consts;
using Domain.Filters;
using Sieve.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
	public class TourHour : BaseModel
    {
        public int Id { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        [MaxLength(150, ErrorMessage = Errors.MaxLength)]
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        [Sieve(CanFilter = true, CanSort = true)]
        public decimal Price { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public string Location { get; set; } = null!;
        [Range(1, 23, ErrorMessage = Errors.MaxHourDuration)]
        public int Duration { get; set; }
        public string ImageUrl { get; set; } = null!;
        public DateTime StartDate { get; set; }
        [DateGreaterThan("StartDate")]
        public DateTime EndDate { get; set; }
        public int? PersonId { get; set; }
        public ApplicationUser? Person { get; set; }

    }
}
