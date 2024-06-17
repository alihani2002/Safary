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

        public ReviewSummary GetReviews(int tourGuideId)
        {
            var reviews = _context.Reviews
                .Include(r => r.Tourist)
                .Where(r => r.TourGuideId == tourGuideId.ToString())
                .ToList();

            double averageRating = reviews.Any() ? reviews.Average(r => r.Rating) : 0;

            return new ReviewSummary
            {
                AverageRating = averageRating,
                Reviews = reviews
            };
        }
    }
}
