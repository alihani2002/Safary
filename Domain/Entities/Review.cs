using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Review
    {
        [Key]
        public int Id { get; set; }
        public int? TouristId { get; set; } // Foreign key to ApplicationUser (Tourist)
        [ForeignKey("TouristId")]
        public ApplicationUser? Tourist { get; set; }
        public int? TourGuideId { get; set; } // Foreign key to ApplicationUser (TourGuide)
        [ForeignKey("TourGuideId")]
        public ApplicationUser? TourGuide { get; set; }
        public double Rating { get; set; }
        public string Comment { get; set; }
     
    }
}
