using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TourReview : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public double Rating { get; set; }
        public string Comment { get; set; }

        [ForeignKey("Tour")]
        public string TourId { get; set; }
        public Tour? Tour { get; set; }

        [ForeignKey("Tourist")]
        public string TouristId { get; set; }
        public ApplicationUser? Tourist { get; set; }
    }
}
