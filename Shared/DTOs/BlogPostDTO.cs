using Domain.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    public class BlogPostDTO
    {
        public string Title { get; set; } = null!;
        public string CoverImage { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Content { get; set; } = null!;
        public int Duration { get; set; }

    }
}
