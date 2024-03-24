using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Places : BaseModel
    {
        public int Id { get; set; }
        [MaxLength(150, ErrorMessage = "Username cannot exceed 150 characters")]
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public string Location { get; set; } = null!;
        public int TourId { get; set; }
        public int CountryId { get; set; } 

    }
}
