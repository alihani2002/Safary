using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    public class ReviewDTO : BaseDTO
    {
        public class ReviewPostDto
        {
            public int Id { get; set; }
            public double Rating { get; set; }
            public string Comment { get; set; }
            public string TourGuideId { get; set; }
            public string TouristId { get; set; }
            public int? TourId { get; set; }
        }
    }
}
