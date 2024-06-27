using Sieve.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    public class ApplicationUsersDTO : RegisterDTO
    {
      
        public bool IsDeleted { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public bool AdminAccepted { get; set; } = false;
        public string? CvUrl { get; set; }
        public string? ImageUrl { get; set; }

        // TourGuide
        public string? Description { get; set; }
        public double Rate { get; set; }
        public decimal HourPrice { get; set; }
        public int Age { get; set; }
        public string? Bio { get; set; }
        public List<string>? LanguageSpoken { get; set; } = new List<string>();
    }
}
