using Domain.Consts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Place : BaseModel
    {
        public int Id { get; set; }
        [MaxLength(150, ErrorMessage = Errors.MaxLength)]
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public string Location { get; set; } = null!;
        public int CountryId { get; set; } 
        public Country? Country { get; set; }
        public int TourHourId { get; set; }
        public TourHour? TourHour { get; set; }
        public int TourDayId { get; set; }
        public TourDay? TourDay { get; set; }

    }
}
