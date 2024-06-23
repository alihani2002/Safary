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
		IBaseRepository<Tour> Tours { get; }
		IBaseRepository<TourImage> TourImages { get; }
		IBaseRepository<TourBlog> TourBlogs { get; }	
        IBaseRepository<TourGuideReview> TourGuideReviews { get; }
        IBaseRepository<TourReview> TourReviews { get; }
        IBaseRepository<SelectedTourGuide> SelectedTourGuides { get; }

		int Complete();
	}
}
