using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    public class TourReviewPostDTO
    {
        [Range(1, 5)]
        public double Rating { get; set; }

        [MaxLength(1000)]
        public string Comment { get; set; }

        [Required]
        public int TourId { get; set; }
    }
}
