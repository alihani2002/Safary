using Domain.Entities;
using Domain.Repositories;
using Persistence.Data;
using Persistence.Repositories;
using Service.Abstractions;

namespace Safary.Repository
{
    public class TourService : BaseRepository<Tour>, ITourService
    {
        public readonly IUnitOfWork _unitOfWork;

        public TourService(ApplicationDbContext context, IUnitOfWork unitOfWork) : base(context)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Tour?> ToggleStatus(string name)
        {
            var tour = await _unitOfWork.Tours.GetByName(name);

            if (tour is null) return null;

            tour.IsDeleted = !tour.IsDeleted;

            _unitOfWork.Complete();

            return tour;
        }
    }
}
