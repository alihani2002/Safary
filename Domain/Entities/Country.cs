using Sieve.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Country: BaseModel
    {
        public int Id { get; set; }
        [MaxLength(150)]
        [Sieve(CanFilter = true, CanSort = true)]
        public string Name { get; set; } = null!;
        public ICollection<City>? Cities { get; set; }
    }
}
