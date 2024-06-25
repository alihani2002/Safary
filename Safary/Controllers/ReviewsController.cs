using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Abstractions;
using Shared.DTOs;

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
        public async Task<ActionResult> GetTourReviews()
        {
            var reviews = await _reviewService.GetAllTourReviewsAsync();
            return Ok(reviews);
        }



        // POST: api/Reviews/TourReviews

        [HttpPost("TourReviews")]
        [Authorize("UserPolicy")]
        public async Task<ActionResult> PostTourReview(TourReviewPostDTO reviewPostDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var review = await _reviewService.AddTourReviewAsync(reviewPostDto);
            return Ok(review);
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
        public async Task<ActionResult> GetTourGuideReviews()
        {
            var reviews = await _reviewService.GetAllTourGuideReviewsAsync();
            return Ok(reviews);
        }



        // POST: api/Reviews/TourGuideReviews
        [HttpPost("TourGuideReviews")]
        [Authorize("UserPolicy")]

        public async Task<ActionResult> PostTourGuideReview(TourGuideReviewPostDto reviewPostDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var review = await _reviewService.AddTourGuideReviewAsync(reviewPostDto);
            return Ok(review);
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
