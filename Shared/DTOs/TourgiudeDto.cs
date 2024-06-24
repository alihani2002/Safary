using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    //alixsomaa@gmail.com
    public class TourgiudeDto : RegisterDTO
    {
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public string? CvUrl { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
        public double Rate { get; set; }
        public decimal HourPrice { get; set; }
        public int Age { get; set; }
        public string? Bio { get; set; }
        public List<string>? LanguageSpoken { get; set; } = new List<string>();
        public bool HasCar { get; set; } = false;
    }
}
