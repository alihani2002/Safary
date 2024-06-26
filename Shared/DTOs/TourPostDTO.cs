using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    public class TourPostDTO
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string CoverImage { get; set; } = null!;
        public int? BlogId { get; set; }

    }
}
