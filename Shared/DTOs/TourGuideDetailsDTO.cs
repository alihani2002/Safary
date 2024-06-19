using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
	public class TourGuideDetailsDTO
	{
        public string FullName { get; set; }
		public string? ImageUrl { get; set; }
		public string? Description { get; set; }
		public double Rate { get; set; }
		public decimal HourPrice { get; set; }
		public int Age { get; set; }
		public string? Bio { get; set; }
		public List<string>? LanguageSpoken { get; set; } = new List<string>();
		public bool HasCar { get; set; } = false;		
		public int? ReviewsNumber { get; set; }
        public TourGuideSelectedDTO TourGuideSelectedDTO { get; set; }

    }
}
