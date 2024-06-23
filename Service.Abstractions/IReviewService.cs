using Domain.Entities;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstractions
{
    public interface IReviewService
    {
        public interface IReviewService
        {
            // Methods for TourReview
            Task<IEnumerable<ReviewDTO>> GetAllTourReviewsAsync();
            Task<ReviewDTO> GetTourReviewByIdAsync(int id);
            Task<ReviewDTO> AddTourReviewAsync(ReviewPostDto reviewPostDto);
            Task<bool> DeleteTourReviewAsync(int id);

            // Methods for TourGuideReview
            Task<IEnumerable<ReviewDTO>> GetAllTourGuideReviewsAsync();
            Task<ReviewDTO> GetTourGuideReviewByIdAsync(int id);
            Task<ReviewDTO> AddTourGuideReviewAsync(ReviewPostDto reviewPostDto);
            Task<bool> DeleteTourGuideReviewAsync(int id);
        }
    }
}
