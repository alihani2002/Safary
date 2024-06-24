using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class TourGuideReview : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public double Rating { get; set; }
        public string Comment { get; set; }

        [ForeignKey("TourGuide")]
        public string? TourGuideId { get; set; }
        public ApplicationUser? TourGuide { get; set; }

        [ForeignKey("Tourist")]
        public string? TouristId { get; set; }
        public ApplicationUser? Tourist { get; set; }
    }
}
