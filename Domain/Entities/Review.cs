using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Review
    {
        [Key]
        public int Id { get; set; }
        public double Rating { get; set; }
        public string Comment { get; set; }
        public string ReviewerName { get; set; } = null!;

        [ForeignKey("User")]
        public string? UserEmail { get; set; } // Foreign key to ApplicationUser (Tourist)
        public ApplicationUser? User { get; set; }
    }
}
