using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    public class TourHourPostDTO
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Description { get; set; } = null!;
        public string Location { get; set; } = null!;
        public int Duration { get; set; }
        public ICollection<TourImageDTO>? TourImages { get; set; }
    }
}
