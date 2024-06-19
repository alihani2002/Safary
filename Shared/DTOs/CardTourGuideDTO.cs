using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
	public class CardTourGuideDTO
	{
        public string? Id { get; set; }
        public string FullName { get; set; } = null!;
		public double Rate { get; set; }
		public decimal HourPrice { get; set; }
		public List<string> LanguageSpoken { get; set; } = new List<string>();
        public bool HasCar { get; set; }
        public int ReviewsNumber { get; set; }
    }
}
