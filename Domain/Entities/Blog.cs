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

        [MaxLength(150, ErrorMessage = "Username cannot exceed 150 characters")]
        public string Name { get; set; } = null!;
        [Required]
        public string Description { get; set; } = null!;
        public DateTime Time { get; set; }
        public string Content { get; set; } = null!;

    }
}
