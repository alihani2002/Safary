using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    public class TourGuideDTO: UserDTO
    {
        public string CVUrl { get; set; }
        public string ImageUrl { get; set; }
    }
}
