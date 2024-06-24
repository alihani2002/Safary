using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class TourReview : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public double Rating { get; set; }
        public string Comment { get; set; }

        [ForeignKey("Tour")]
        public string? TourName { get; set; }
        public Tour? Tour { get; set; }

        [ForeignKey("Tourist")]
        public string? TouristId { get; set; }
        public ApplicationUser? Tourist { get; set; }
    }
}
