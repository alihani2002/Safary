using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    public class TourgiudeDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public bool AdminAccepted { get; set; } = false;
        public string? CvUrl { get; set; }
        public string? ImageUrl { get; set; }

        // TourGuide
        public string? Description { get; set; }
        public double Rate { get; set; }
        public decimal DayPrice { get; set; }
        public decimal HourPrice { get; set; }
        public int Age { get; set; }
        public string? Bio { get; set; }
        public List<string>? LanguageSpoken { get; set; } = new List<string>();
    }
}
