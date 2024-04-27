using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    public class BlogPostDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string CoverImage { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime Time { get; set; }
        public string Content { get; set; } = null!;
    }
}
