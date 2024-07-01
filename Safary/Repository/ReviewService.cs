using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Service.Abstractions;
using Shared.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Service.Implementations
{
    public class ReviewService : IReviewService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ReviewService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

       
        // Tour Reviews Methods
        public async Task<IEnumerable<TourReviewDTO>> GetAllTourReviewsAsync()
        {
            var reviews = await _unitOfWork.TourReviews.GetAll();
            var reviewDTOs = _mapper.Map<IEnumerable<TourReviewDTO>>(reviews);
            var averageRating = (int)reviews.Average(r => r.Rating);

            foreach (var review in reviewDTOs)
            {
                review.AverageRating = averageRating;
            }

            return reviewDTOs;
        }

        public async Task<TourReviewDTO> GetTourReviewByIdAsync(int id)
        {
            var review = await _unitOfWork.TourReviews.GetById(id);
            var reviewDTO = _mapper.Map<TourReviewDTO>(review);
            var allReviews = await _unitOfWork.TourReviews.GetAll();
            reviewDTO.AverageRating = (int)allReviews.Average(r => r.Rating);
            return reviewDTO;
        }

        public async Task<TourReviewDTO> AddTourReviewAsync(TourReviewPostDTO reviewPostDto)
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue("uid")!;
            var review = _mapper.Map<TourReview>(reviewPostDto);
            review.TouristId = userId;

            await _unitOfWork.TourReviews.Add(review);
            _unitOfWork.Complete();

            return _mapper.Map<TourReviewDTO>(review);
        }

        public async Task<bool> DeleteTourReviewAsync(int id)
        {
            var review = await _unitOfWork.TourReviews.GetById(id);
            if (review == null)
                return false;

            _unitOfWork.TourReviews.Remove(review);
            _unitOfWork.Complete();
            return true;
        }

        // Tour Guide Reviews Methods
        public async Task<IEnumerable<TourGuideReviewDTO>> GetAllTourGuideReviewsAsync()
        {
            var reviews = await _unitOfWork.TourGuideReviews.GetAll();
            var reviewDTOs = _mapper.Map<IEnumerable<TourGuideReviewDTO>>(reviews);
            var averageRating = (int)reviews.Average(r => r.Rating);

            foreach (var review in reviewDTOs)
            {
                review.AverageRating = averageRating;
            }

            return reviewDTOs;

        }



        public async Task<TourGuideReviewDTO> GetTourGuideReviewByIdAsync(int id)
        {
            var review = await _unitOfWork.TourGuideReviews.GetById(id);
            return _mapper.Map<TourGuideReviewDTO>(review);
        }

        public async Task<TourGuideReviewDTO> AddTourGuideReviewAsync(TourGuideReviewPostDto reviewPostDto)
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue("uid")!;
            var review = _mapper.Map<TourGuideReview>(reviewPostDto);
            review.TouristId = userId;

            await _unitOfWork.TourGuideReviews.Add(review);
            _unitOfWork.Complete();

            return _mapper.Map<TourGuideReviewDTO>(review);
        }

        public async Task<bool> DeleteTourGuideReviewAsync(int id)
        {
            var review = await _unitOfWork.TourGuideReviews.GetById(id);
            if (review == null)
                return false;

            _unitOfWork.TourGuideReviews.Remove(review);
             _unitOfWork.Complete();
            return true;

        }

        public async Task<TourReview?> ToggleStatus(int id)
        {
            var tour = await _unitOfWork.TourReviews.GetById(id);

            if (tour is null) return null;

            tour.IsDeleted = !tour.IsDeleted;

            _unitOfWork.Complete();

            return tour;
        }

        public async Task<TourGuideReview?> GuideToggleStatus(int id)
        {
            var tour = await _unitOfWork.TourGuideReviews.GetById(id);

            if (tour is null) return null;

            tour.IsDeleted = !tour.IsDeleted;

            _unitOfWork.Complete();

            return tour;
        }

        
    }
}
