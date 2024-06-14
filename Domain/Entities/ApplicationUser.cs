using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Sieve.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Index(nameof(Email), IsUnique = true)]
    [Index(nameof(UserName), IsUnique = true)]
    public class ApplicationUser: IdentityUser 
    {
        [MaxLength(150)]
        [Sieve(CanFilter = true, CanSort = true)]
        public string FirstName { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        [MaxLength(150)]
        public string LastName { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public string FullName { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public string Address { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public bool IsDeleted { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public bool AdminAccepted { get; set; } = false;
        public string? CvUrl { get; set; }
        public string? ImageUrl { get; set; }

        // TourGuide
        public string? Description { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public double Rate { get; set; }
        public decimal DayPrice { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public decimal HourPrice { get; set; }
        public int Age { get; set; }
        public string? Bio { get; set; }
        public List<string>? LanguageSpoken { get; set; } = new List<string>();
        public int? TourGuideId { get; set; }
        public int? TouristId { get; set; }
        public int? BlogId { get; set; }    
        public Blog? Blog { get; set; }
        public int? TourDayId { get; set; }
        public TourDay? TourDay { get; set; }
        public int? TourHourId { get; set; }
        public TourHour? TourHour { get; set; }

    }
}
