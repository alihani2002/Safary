using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Abstractions;

namespace Safary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet("{tourGuideId}")]
        public IActionResult GetReviews(int tourGuideId)
        {
            var reviewSummary = _reviewService.GetReviews(tourGuideId);
            return Ok(reviewSummary);
        }
    }
}
