using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
	public interface IUnitOfWork
	{
        IBaseRepository<ApplicationUser> ApplicationUsers { get; }
        IBaseRepository<Blog> Blogs { get; }
		IBaseRepository<TourDay> TourDays { get; }
		IBaseRepository<TourHour> TourHours { get; }
		IBaseRepository<Place> Places { get; }
		IBaseRepository<Country> Countries { get; }
		IBaseRepository<Tour> Tours { get; }	
		IBaseRepository<City> Cities { get; }
        IBaseRepository<Review> Reviews { get; }

        int Complete();
	}
}
