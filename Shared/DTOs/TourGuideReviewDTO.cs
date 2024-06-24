namespace Shared.DTOs
{
    public class TourGuideReviewDTO : BaseDTO
    {
        public string Id { get; set; }
        public double Rating { get; set; }
        public string Comment { get; set; }
        public string TourGuideId { get; set; }
        public string TouristId { get; set; }
    }

}
