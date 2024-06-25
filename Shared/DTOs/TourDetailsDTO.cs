
namespace Shared.DTOs
{
    public class TourDetailsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public int Duration { get; set; }
        public double AverageRating { get; set; }
        public List<TourReviewDetailsDTO> Reviews { get; set; } = new List<TourReviewDetailsDTO>();
        public ICollection<TourImagesDTO>? TourImages { get; set; }
        public int NumberOfReviews { get; set; }


    }
}
