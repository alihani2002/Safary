using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
	public class CardTourGuideDTO
	{
        public string? Id { get; set; }
        public string FullName { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public double Rate { get; set; }
		public decimal HourPrice { get; set; }
		public List<string> LanguageSpoken { get; set; } = new List<string>();
        public bool IsDeleted { get; set; }
        public bool HasCar { get; set; }
        public int ReviewsNumber { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
