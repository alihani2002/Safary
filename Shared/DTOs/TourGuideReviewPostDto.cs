using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs
{
    public class TourGuideReviewPostDto 
    {
        [Range(1, 5)]
        public double Rating { get; set; }

        [MaxLength(1000)]
        public string Comment { get; set; }

        [Required]
        public string TourGuideId { get; set; }
    }
}
