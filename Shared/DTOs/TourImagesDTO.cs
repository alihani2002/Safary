using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    public class TourImagesDTO
    {
        public int TourId { get; set; }  // The ID of the tour to which images will be added
        public List<string> ImageUrls { get; set; } = new List<string>();  // List of image URLs to be added
    }
}
