using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
	public class RegisterTourGuideDTO: RegisterDTO
	{
		public string CV { get; set; } = null!;
		public string Description { get; set; } = null!;
		public decimal DayPrice { get; set; }
		public decimal HourPrice { get; set; }
		public int Age { get; set; }
		public string Bio { get; set; } = null!;
		public List<string> LanguageSpoken { get; set; } = new List<string>();
	}
}
