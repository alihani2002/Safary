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
        //double GetAverageRating(string tourGuideId);
        //List<Review> GetReviewsByTourGuideId(string tourGuideId);
        Task<IEnumerable<ReviewDTO>> GetAllReviewsAsync();
        Task<ReviewDTO> GetReviewByIdAsync(int id);
        Task<ReviewDTO> AddReviewAsync(ReviewPostDto reviewPostDto);
        Task<bool> DeleteReviewAsync(int id);
    }
}
