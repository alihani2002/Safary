using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstractions
{
    public interface IReviewService
    {
        double GetAverageRating(int tourGuideId);
        List<Review> GetReviewsByTourGuideId(int tourGuideId);
    }
}
