using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    public class CountryDTO
    {
        public int Id { get; set; }
        [MaxLength(150)]
        public string Name { get; set; } = null!;
        public ICollection<City>? Cities { get; set; }
    }
}
