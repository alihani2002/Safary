using Microsoft.AspNetCore.Mvc;
using Service.Abstractions;
using Shared.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        // GET: api/Reviews/TourReviews
        [HttpGet("TourReviews")]
        public async Task<ActionResult<IEnumerable<TourReviewDTO>>> GetTourReviews()
        {
            var reviews = await _reviewService.GetAllTourReviewsAsync();
            return Ok(reviews);
        }

        // GET: api/Reviews/TourReviews/5
        [HttpGet("TourReviews/{id}")]
        public async Task<ActionResult<TourReviewDTO>> GetTourReview(int id)
        {
            var review = await _reviewService.GetTourReviewByIdAsync(id);
            if (review == null)
                return NotFound();

            return Ok(review);
        }

        // POST: api/Reviews/TourReviews
        [HttpPost("TourReviews")]
        public async Task<ActionResult<TourReviewDTO>> PostTourReview(TourReviewPostDTO reviewPostDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var review = await _reviewService.AddTourReviewAsync(reviewPostDto);
            return CreatedAtAction(nameof(GetTourReview), new { id = review.Id }, review);
        }

        // DELETE: api/Reviews/TourReviews/5
        [HttpDelete("TourReviews/{id}")]
        public async Task<IActionResult> DeleteTourReview(int id)
        {
            var success = await _reviewService.DeleteTourReviewAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }

        // GET: api/Reviews/TourGuideReviews
        [HttpGet("TourGuideReviews")]
        public async Task<ActionResult<IEnumerable<TourGuideReviewDTO>>> GetTourGuideReviews()
        {
            var reviews = await _reviewService.GetAllTourGuideReviewsAsync();
            return Ok(reviews);
        }

        // GET: api/Reviews/TourGuideReviews/5
        [HttpGet("TourGuideReviews/{id}")]
        public async Task<ActionResult<TourGuideReviewDTO>> GetTourGuideReview(int id)
        {
            var review = await _reviewService.GetTourGuideReviewByIdAsync(id);
            if (review == null)
                return NotFound();

            return Ok(review);
        }

        // POST: api/Reviews/TourGuideReviews
        [HttpPost("TourGuideReviews")]
        public async Task<ActionResult<TourGuideReviewDTO>> PostTourGuideReview(TourGuideReviewPostDto reviewPostDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var review = await _reviewService.AddTourGuideReviewAsync(reviewPostDto);
            return CreatedAtAction(nameof(GetTourGuideReview), new { id = review.Id }, review);
        }

        // DELETE: api/Reviews/TourGuideReviews/5
        [HttpDelete("TourGuideReviews/{id}")]
        public async Task<IActionResult> DeleteTourGuideReview(int id)
        {
            var success = await _reviewService.DeleteTourGuideReviewAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
