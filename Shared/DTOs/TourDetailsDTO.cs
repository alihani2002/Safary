
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
        public ICollection<TourImageDetailsDTO>? TourImages { get; set; }
        public int NumberOfReviews { get; set; }
        public bool IsDeleted { get; set; }

    }

    public class TourImageDetailsDTO
    {
        public string ImageUrl { get; set; } = null!;
    }
}
