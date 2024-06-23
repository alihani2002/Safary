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
        // Tour Reviews Methods
        Task<IEnumerable<TourReviewDTO>> GetAllTourReviewsAsync();
        Task<TourReviewDTO> GetTourReviewByIdAsync(int id);
        Task<TourReviewDTO> AddTourReviewAsync(TourReviewPostDTO reviewPostDto);
        Task<bool> DeleteTourReviewAsync(int id);

        // Tour Guide Reviews Methods
        Task<IEnumerable<TourGuideReviewDTO>> GetAllTourGuideReviewsAsync();
        Task<TourGuideReviewDTO> GetTourGuideReviewByIdAsync(int id);
        Task<TourGuideReviewDTO> AddTourGuideReviewAsync(TourGuideReviewPostDto reviewPostDto);
        Task<bool> DeleteTourGuideReviewAsync(int id);
    }
}

