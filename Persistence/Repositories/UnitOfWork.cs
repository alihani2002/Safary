using Domain.Entities;
using Domain.Repositories;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
	public class UnitOfWork: IUnitOfWork
	{
		private readonly ApplicationDbContext _context;

		public UnitOfWork(ApplicationDbContext context)
		{
			_context = context;
		}
        public IBaseRepository<ApplicationUser> ApplicationUsers => new BaseRepository<ApplicationUser>(_context);
        public IBaseRepository<SelectedTourGuide> SelectedTourGuides => new BaseRepository<SelectedTourGuide>(_context);

        public IBaseRepository<Blog> Blogs => new BaseRepository<Blog>(_context);

		public IBaseRepository<TourHour> TourHours =>  new BaseRepository<TourHour>(_context);

		public IBaseRepository<Tour> Tours =>  new BaseRepository<Tour>(_context);

        public IBaseRepository<Review> Reviews => new BaseRepository<Review>(_context);


		public int Complete()
		{
			return _context.SaveChanges();
		}
	}
}
