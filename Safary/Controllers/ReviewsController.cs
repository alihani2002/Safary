using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Safary.Repository;
using Service.Abstractions;
using Shared.DTOs;
using Sieve.Services;

namespace Safary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReviewsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: api/Reviews/tourGuideId
        [HttpGet("{tourGuideId}")]
        public async Task<ActionResult<IEnumerable<ReviewDTO>>> GetReviewsByTourGuideId(int tourGuideId)
        {
            var reviews = await _unitOfWork.Reviews.FindAll(r => r.UserID == tourGuideId.ToString() , 0);
            var reviewDtos = _mapper.Map<IEnumerable<ReviewDTO>>(reviews);
            return Ok(reviewDtos);
        }

        // GET: api/Reviews/averageRating/tourGuideId
        [HttpGet("averageRating/{tourGuideId}")]
        public async Task<ActionResult<double>> GetAverageRating(int tourGuideId)
        {
            var reviews = await _unitOfWork.Reviews.FindAll(r => r.UserID == tourGuideId.ToString(), 0);
            var averageRating = reviews.Any() ? reviews.Average(r => r.Rating) : 0;
            return Ok(averageRating);
        }


        // POST: api/Reviews
        [HttpPost]
        public async Task<ActionResult<ReviewDTO>> CreateReview([FromBody] ReviewDTO reviewDto)
        {
            if (reviewDto == null)
            {
                return BadRequest();
            }

            var review = _mapper.Map<Review>(reviewDto);
            await _unitOfWork.Reviews.Add(review);
             _unitOfWork.Complete();

            var createdReviewDto = _mapper.Map<ReviewDTO>(review);
            return CreatedAtAction(nameof(GetReviewsByTourGuideId), new { tourGuideId = review.UserID }, createdReviewDto);
        }

        // PUT: api/Reviews/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateReview(int id, [FromBody] ReviewDTO reviewDto)
        {
            if (id != reviewDto.Id)
            {
                return BadRequest();
            }

            var existingReview = await _unitOfWork.Reviews.GetById(id);
            if (existingReview == null)
            {
                return NotFound();
            }

            _mapper.Map(reviewDto, existingReview);
            _unitOfWork.Reviews.Update(existingReview);
            _unitOfWork.Complete();

            return NoContent();
        }

        // DELETE: api/Reviews/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteReview(int id)
        {
            var review = await _unitOfWork.Reviews.GetById(id);
            if (review == null)
            {
                return NotFound();
            }

            _unitOfWork.Reviews.Remove(review);
            _unitOfWork.Complete();

            return NoContent();
        }
    }
}
