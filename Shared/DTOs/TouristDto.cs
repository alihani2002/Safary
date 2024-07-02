using Sieve.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    public class TouristDto : RegisterDTO
    {
        public string? Id { get; set; }
        
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public int Age { get; set; }
        public string? Bio { get; set; }
        public bool IsDeleted { get; set; }
        public string? ImageUrl { get; set; }
    }
}
