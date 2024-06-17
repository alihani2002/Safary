using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    public class TouristDto
    {
        public string Id { get; set; } // Assuming string ID for IdentityUser
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string? ImageUrl { get; set; }
        // Other properties as needed
    }
}
