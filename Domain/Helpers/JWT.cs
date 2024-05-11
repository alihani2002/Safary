using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Helpers
{
    public class JWT
    {
        public string ValidIssuer { get; set; }
        public string ValidAudiance { get; set; }
        public double DurationInDays { get; set; }
        public string Key { get; set; }
    }
}
