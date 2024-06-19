using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
	public class SelectedTourGuide
	{
		public int Id { get; set; }
		public string TourGuideId { get; set; }
		[ForeignKey("TourGuideId")]
		public ApplicationUser? ApplicationUser { get; set; }
		public string UserId { get; set; }
		public DateTime? SelectedDate { get; set; }
		public TimeSpan? SelectedTime { get; set; }
		public int Adults { get; set; } = 1;
	}
}
