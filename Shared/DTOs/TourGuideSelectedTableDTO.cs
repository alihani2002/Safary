using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
	public class TourGuideSelectedTableDTO
	{
		public int Id { get; set; }
		public string TouristId { get; set; }
		public string TourguideId { get; set; }
		public DateTime SelectedDate { get; set; }
		public TimeOnly SelectedTime { get; set; }
		public int Adults { get; set; } = 1;
		public bool IsConfirmed { get; set; } = false;
		public string? TourName { get; set; }
	}
}
