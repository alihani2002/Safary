using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    public class CreateTourDTO
    {
        public string Name { get; set; }
        public string Description { get; set; } = null!;
        public string Location { get; set; } = null!;
        public int Duration { get; set; }
    }
}
