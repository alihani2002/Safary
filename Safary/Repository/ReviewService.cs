using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using Service.Abstractions;
using Shared.DTOs;

namespace Safary.Repository
{
    public class ReviewService : IReviewService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReviewService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // TourReview CRUD operations
        public async Task<IEnumerable<ReviewDTO>> GetAllTourReviewsAsync()
        {
            var reviews = await _unitOfWork.TourReviews.GetAll();
            return _mapper.Map<IEnumerable<ReviewDTO>>(reviews);
        }

        public async Task<ReviewDTO> GetTourReviewByIdAsync(int id)
        {
            var review = await _unitOfWork.TourReviews.GetById(id);
            return _mapper.Map<ReviewDTO>(review);
        }

        public async Task<ReviewDTO> AddTourReviewAsync(ReviewPostDto reviewPostDto)
        {
            var review = _mapper.Map<TourReview>(reviewPostDto);
            await _unitOfWork.TourReviews.Add(review);
            _unitOfWork.Complete(); // Ensure to save changes asynchronously
            return _mapper.Map<ReviewDTO>(review);
        }

        public async Task<bool> DeleteTourReviewAsync(int id)
        {
            var review = await _unitOfWork.TourReviews.GetById(id);
            if (review == null) return false;

            _unitOfWork.TourReviews.Remove(review);
            _unitOfWork.Complete(); // Ensure to save changes asynchronously
            return true;
        }

        // TourGuideReview CRUD operations
        public async Task<IEnumerable<ReviewDTO>> GetAllTourGuideReviewsAsync()
        {
            var reviews = await _unitOfWork.TourGuideReviews.GetAll();
            return _mapper.Map<IEnumerable<ReviewDTO>>(reviews);
        }

        public async Task<ReviewDTO> GetTourGuideReviewByIdAsync(int id)
        {
            var review = await _unitOfWork.TourGuideReviews.GetById(id);
            return _mapper.Map<ReviewDTO>(review);
        }

        public async Task<ReviewDTO> AddTourGuideReviewAsync(ReviewPostDto reviewPostDto)
        {
            var review = _mapper.Map<TourGuideReview>(reviewPostDto);
            await _unitOfWork.TourGuideReviews.Add(review);
             _unitOfWork.Complete(); // Ensure to save changes asynchronously
            return _mapper.Map<ReviewDTO>(review);
        }

        public async Task<bool> DeleteTourGuideReviewAsync(int id)
        {
            var review = await _unitOfWork.TourGuideReviews.GetById(id);
            if (review == null) return false;

            _unitOfWork.TourGuideReviews.Remove(review);
            _unitOfWork.Complete(); // Ensure to save changes asynchronously
            return true;
        }
    }
}