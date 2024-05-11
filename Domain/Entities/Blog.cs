using Domain.Consts;
using Domain.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Blog : BaseModel
    {
        public int Id { get; set; }

        [MaxLength(150, ErrorMessage = Errors.MaxLength)]
        public string Title { get; set; } = null!;
        public string CoverImage { get; set; } = null!;
        public string Description { get; set; } = null!;
		[Range(1, 30)]
		public int Duration { get; set; }
		public DateTime StartDate { get; set; }
		[DateGreaterThan("StartDate")]
		public DateTime EndDate { get; set; }
		public string Content { get; set; } = null!;
        public ICollection<Tour>? Tours { get; set; } = new List<Tour>();
        public ICollection<ApplicationUser>? TouristsAndTourGuides { get; set; } = new List<ApplicationUser>();
    }
}
