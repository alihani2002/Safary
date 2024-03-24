using Domain.Consts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TourDay : BaseModel
    {
        public int Id { get; set; }

        [MaxLength(150, ErrorMessage = Errors.MaxLength)]
        public string Name { get; set; } = null!;
        
        public decimal Price { get; set; }
        
        public string Description { get; set; } = null!;
        
        [Range(1, 30)]
        public int Duration { get; set; }
        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }
        public ICollection<Places> Places { get; set; }
        public int TourGuideId { get; set; }
        public TourGuide TourGuide { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }


    }
}
