using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    public class ReviewPostDto 
    {
        public double Rating { get; set; }
        public string Comment { get; set; }
        public string ReviewerName { get; set; } // email toursit
        public string UserEmail { get; set; } // email tourguide
    }
}
