using Domain.Consts;
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
        public DateTime Time { get; set; }
        public string Content { get; set; } = null!;

    }
}
