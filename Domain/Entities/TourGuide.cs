using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TourGuide : BaseModel
    {
        public int Id { get; set; }

        [MaxLength(150, ErrorMessage = "Username cannot exceed 150 characters")]
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public double Rate { get; set; }
        public decimal DayPrice { get; set; }
        public decimal HourPrice { get; set; }
        public int Age { get; set; }
        public string Bio { get; set; }
        public string LanguageSpoken { get; set; }
        public string ImageUrl { get; set;}


    }
}
