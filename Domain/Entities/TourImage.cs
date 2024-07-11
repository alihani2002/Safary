using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TourImage
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; } = null!;
        public string? TourName { get; set; }
        public Tour? Tour { get; set; }
    }
}
