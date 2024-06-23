using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
	public class RegisterTourGuideDTO: RegisterDTO
	{
		public IFormFile CV { get; set; } = null!;
		public string Description { get; set; } = null!;
		public decimal HourPrice { get; set; } = default!;
		public IFormFile Image { get; set; } = null!;
		public int Age { get; set; } = default!;
		public string Bio { get; set; } = null!;
		public List<string> LanguageSpoken { get; set; } = new List<string>();
		public bool HasCar { get; set; } = false;
	}
}
