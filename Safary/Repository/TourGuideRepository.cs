using Domain.Entities;
using Persistence.Data;
using Persistence.Repositories;
using Service.Abstractions;

namespace Safary.Repository
{
	public class TourGuideRepository : BaseRepository<ApplicationUser>, ITourGuideRepository
	{

		public TourGuideRepository(ApplicationDbContext context) : base(context)
		{

		}
		// !(specificStartDate <= user.EndDate && specificEndDate >= user.StartDate)

		public IEnumerable<ApplicationUser> GetAll(DateTime from, DateTime to) =>
			 _context.Set<ApplicationUser>().Where(g => !(g.StartDate <= to && g.EndDate >= from))
				.ToList();



		//public IEnumerable<ApplicationUser> GetAll(DateTime from, DateTime to) =>
		//	 _context.Set<ApplicationUser>().Where(g => !(g.StartDate <= to && g.EndDate >= to) || !(g.StartDate <= from && g.EndDate >= from))
		//		.ToList();
	}
}
