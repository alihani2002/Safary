using Microsoft.AspNetCore.Mvc;
using Safary.Repository;
using Service.Abstractions;
using Shared.DTOs;

namespace Safary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly ReviewService _reviewService;

        public ReviewsController(ReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        // CRUD operations for TourReview

        // GET: api/Reviews/tour
        [HttpGet("tour")]
        public async Task<ActionResult<IEnumerable<ReviewDTO>>> GetTourReviews()
        {
            var reviews = await _reviewService.GetAllTourReviewsAsync();
            return Ok(reviews);
        }

        // GET: api/Reviews/tour/5
        [HttpGet("tour/{id}")]
        public async Task<ActionResult<ReviewDTO>> GetTourReview(int id)
        {
            var review = await _reviewService.GetTourReviewByIdAsync(id);

            if (review == null)
            {
                return NotFound($"Tour review with ID {id} not found");
            }

            return Ok(review);
        }

        // POST: api/Reviews/tour
        [HttpPost("tour")]
        public async Task<ActionResult<ReviewDTO>> PostTourReview(ReviewPostDto reviewPostDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var review = await _reviewService.AddTourReviewAsync(reviewPostDto);

            return Ok(review);
        }

        // DELETE: api/Reviews/tour/5
        [HttpDelete("tour/{id}")]
        public async Task<IActionResult> DeleteTourReview(int id)
        {
            var success = await _reviewService.DeleteTourReviewAsync(id);

            if (!success)
            {
                return NotFound($"Tour review with ID {id} not found");
            }

            return NoContent();
        }

        // CRUD operations for TourGuideReview

        // GET: api/Reviews/tourguide
        [HttpGet("tourguide")]
        public async Task<ActionResult<IEnumerable<ReviewDTO>>> GetTourGuideReviews()
        {
            var reviews = await _reviewService.GetAllTourGuideReviewsAsync();
            return Ok(reviews);
        }

        // GET: api/Reviews/tourguide/5
        [HttpGet("tourguide/{id}")]
        public async Task<ActionResult<ReviewDTO>> GetTourGuideReview(int id)
        {
            var review = await _reviewService.GetTourGuideReviewByIdAsync(id);

            if (review == null)
            {
                return NotFound($"Tour guide review with ID {id} not found");
            }

            return Ok(review);
        }

        // POST: api/Reviews/tourguide
        [HttpPost("tourguide")]
        public async Task<ActionResult<ReviewDTO>> PostTourGuideReview(ReviewPostDto reviewPostDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var review = await _reviewService.AddTourGuideReviewAsync(reviewPostDto);

            return Ok(review);
        }

        // DELETE: api/Reviews/tourguide/5
        [HttpDelete("tourguide/{id}")]
        public async Task<IActionResult> DeleteTourGuideReview(int id)
        {
            var success = await _reviewService.DeleteTourGuideReviewAsync(id);

            if (!success)
            {
                return NotFound($"Tour guide review with ID {id} not found");
            }

            return NoContent();
        }
    }
}
