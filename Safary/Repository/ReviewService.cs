using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Service.Abstractions;
using Shared.DTOs;

namespace Safary.Repository
{
    public class ReviewService : IReviewService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;


        public ReviewService(ApplicationDbContext context, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _context = context;

        }

        public async Task<IEnumerable<ReviewDTO>> GetAllReviewsAsync()
        {
            var reviews = await _unitOfWork.Reviews.GetAll();
            return _mapper.Map<IEnumerable<ReviewDTO>>(reviews);
        }

        public async Task<ReviewDTO> GetReviewByIdAsync(int id)
        {
            var review = await _unitOfWork.Reviews.GetById(id);
            return _mapper.Map<ReviewDTO>(review);
        }

        public async Task<double?> GetAverageRatingForTourGuideAsync(string tourGuideEmail)
        {
            var averageRating = await _context.Reviews
                .Where(r => r.User.Email == tourGuideEmail)
                .AverageAsync(r => (double?)r.Rating);

            return averageRating;
        }

        public async Task<ReviewDTO> AddReviewAsync(ReviewPostDto reviewPostDto)
        {
            var user = await _unitOfWork.ApplicationUsers.Find(u => u.Email == reviewPostDto.UserEmail);
            if (user == null)
            {
                throw new Exception("User with the specified email does not exist.");
            }

            var review = _mapper.Map<Review>(reviewPostDto);
            await _unitOfWork.Reviews.Add(review);
            _unitOfWork.Complete();
            return _mapper.Map<ReviewDTO>(review);
        }

        public async Task<bool> DeleteReviewAsync(int id)
        {
            var review = await _unitOfWork.Reviews.GetById(id);
            if (review == null)
                return false;

            _unitOfWork.Reviews.Remove(review);
             _unitOfWork.Complete();
            return true;
        }
    }
}
