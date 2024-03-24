using Domain.Consts;
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

        [MaxLength(150, ErrorMessage = Errors.MaxLength)]
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public double Rate { get; set; }
        public decimal DayPrice { get; set; }
        public decimal HourPrice { get; set; }
        public int Age { get; set; }
        public string Bio { get; set; } = null!;
        public string LanguageSpoken { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;


    }
}
