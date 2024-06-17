using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Service.Abstractions;

namespace Safary.Repository
{
    public class ReviewService : IReviewService
    {
        private readonly ApplicationDbContext _context;

        public ReviewService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Method to calculate the average rating for a specific TourGuide
        public double GetAverageRating(int tourGuideId)
        {
            var reviews = _context.Reviews.Where(r => r.UserID == tourGuideId.ToString());
            if (reviews.Any())
            {
                return reviews.Average(r => r.Rating);
            }
            return 0;
        }

        // Method to get all reviews for a specific TourGuide
        public List<Review> GetReviewsByTourGuideId(int tourGuideId)
        {
            return _context.Reviews
                .Include(r => r.User)
                .Where(r => r.UserID == tourGuideId.ToString())
                .ToList();
        }
    }
}
