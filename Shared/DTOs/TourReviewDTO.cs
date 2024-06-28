using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    public class TourReviewDTO
    {
        public int Id { get; set; }
        public double Rating { get; set; }
        public string Comment { get; set; }
        public string TourName { get; set; }
        public string TouristId { get; set; }
        public bool IsDeleted { get; set; }

    }
}
