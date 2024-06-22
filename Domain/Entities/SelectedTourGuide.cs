using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities
{
	public class SelectedTourGuide
	{
		public int Id { get; set; }
		// Foreign keys
		public string TouristId { get; set; }
		public string TourguideId { get; set; }

		// Navigation properties
		public ApplicationUser? Tourist { get; set; }
		public ApplicationUser? Tourguide { get; set; }
		public DateTime SelectedDate { get; set; }
		public TimeOnly SelectedTime { get; set; }
		public int Adults { get; set; } = 1;
		public bool IsConfirmed { get; set; } = false;
        public string TourName { get; set; }
        public int? TourId { get; set; }
        public TourHour? Tour { get; set; }
    }
}
