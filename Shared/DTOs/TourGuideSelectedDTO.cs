using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
	public class TourGuideSelectedDTO
	{
		public string? Id { get; set; }
		public DateTime? SelectedDate { get; set; }
		public TimeSpan? SelectedTime { get; set; }
		public int Adults { get; set; } = 1;
	}
}
