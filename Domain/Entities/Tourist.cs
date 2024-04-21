using System.ComponentModel.DataAnnotations;
namespace Domain.Entities
{
	public class Tourist :BaseModel
    {
        public int Id { get; set; } 
        public string Name { get; set; } = null!;
        public string Phone { get; set; } = null!;
        [EmailAddress]
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;

    }
}
