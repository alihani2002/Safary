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
            return _mapper.Map<IEnumerable<TourReviewDTO>>(reviews);
        }

        public async Task<TourReviewDTO> GetTourReviewByIdAsync(int id)
        {
            var review = await _unitOfWork.TourReviews.GetById(id);
            return _mapper.Map<TourReviewDTO>(review);
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
            return _mapper.Map<IEnumerable<TourGuideReviewDTO>>(reviews);
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

        //// Method to get all reviews for a specific tour guide by their ID
        //public async Task<TourGuideWithReviewsDTO> GetTourGuideReviewsByGuideIdAsync(string guideId)
        //{
        //    var reviews = await _unitOfWork.TourGuideReviews.Find(r => r.TourGuideId == guideId);

        //    var reviewDTOs = _mapper.Map<IEnumerable<TourGuideReviewDTO>>(reviews).ToList();

        //    return new TourGuideWithReviewsDTO
        //    {
        //        Id = guideId,
        //        Reviews = reviewDTOs
        //    };
        //}

        //public async Task<IEnumerable<TourWithReviewsDTO>> GetTourReviewsByTourNameAsync(string tourName)
        //{
        //    var reviews = await _unitOfWork.TourReviews.Find(r => r.Tour.Name == tourName);
        //    return _mapper.Map<IEnumerable<TourWithReviewsDTO>>(reviews);
        //}
    }
}
