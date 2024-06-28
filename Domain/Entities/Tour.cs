using Domain.Consts;
using Domain.Filters;
using Sieve.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
	public class Tour : BaseModel
    {
        public int Id { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        [MaxLength(150, ErrorMessage = Errors.MaxLength)]
        [Key]
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        [Sieve(CanFilter = true, CanSort = true)]
        public string Location { get; set; } = null!;
        [Range(1, 23, ErrorMessage = Errors.MaxHourDuration)]
        public int Duration { get; set; }
        public ICollection<SelectedTourGuide>? SelectedTourGuides { get; set; }
        public ICollection<TourImage>? TourImages { get; set; }
        public ICollection<TourReview>? Reviews { get; set; }

    }
}
