using Sieve.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class City
    {
        public int Id { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        [Sieve(CanFilter = true, CanSort = true)]
        public string Location { get; set; } = null!;
        public ICollection<Place>? Places { get; set; }
        public int? CountryId { get; set; }
        public Country? Country { get; set; }
    }
}
