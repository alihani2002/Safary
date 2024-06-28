using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    public class AdminDTO
    {
        public string? Id { get; set; }
        public string FullName { get; set; } = null!;
        public bool IsDeleted { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
