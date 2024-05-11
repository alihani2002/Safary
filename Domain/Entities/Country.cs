using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Country: BaseModel
    {
        public int Id { get; set; }
        [MaxLength(150)]
        public string Name { get; set; } = null!;
        public ICollection<City>? Cities { get; set; }
    }
}
