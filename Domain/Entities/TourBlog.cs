using Domain.Consts;
using Domain.Filters;
using Sieve.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
	public class TourBlog
	{
		public int Id { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        [MaxLength(150, ErrorMessage = Errors.MaxLength)]
		public string Name { get; set; } = null!;
		public string Description { get; set; } = null!;
		public int? BlogId { get; set; }
		public Blog? Blog { get; set; }
	}
}
