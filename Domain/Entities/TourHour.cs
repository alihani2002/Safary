using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TourHour : BaseModel
    {
        public int Id { get; set; }
        [MaxLength(150, ErrorMessage = "Username cannot exceed 150 characters")]
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Location { get; set; } = null!;
        [Range(1, 23, ErrorMessage = "Duration Must be more Than 1 Hour less Than 24 Hours")]
        public int Duration { get; set; }
        public string ImageUrl { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}
