using Domain.Entities;
using Domain.Repositories;
using Persistence.Data;
using Persistence.Repositories;
using Service.Abstractions;

namespace Safary.Repository
{
    public class TourBlogService : BaseRepository<TourBlog>, ITourBlogService
    {
        public readonly IUnitOfWork _unitOfWork;

        public TourBlogService(ApplicationDbContext context, IUnitOfWork unitOfWork) : base(context)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TourBlog?> ToggleStatus(int id)
        {
            var tour = await _unitOfWork.TourBlogs.GetById(id);

            if (tour is null) return null;

            tour.IsDeleted = !tour.IsDeleted;

            _unitOfWork.Complete();

            return tour;
        }
    }
}
