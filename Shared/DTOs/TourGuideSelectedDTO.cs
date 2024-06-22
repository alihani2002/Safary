using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
	public class TourGuideSelectedDTO
	{
		public string TourGuideId { get; set; }
		public DateTime SelectedDate { get; set; }
        public string TimeToCast { get; set; }
        public int Adults { get; set; } = 1;
	}
}
