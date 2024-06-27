using Domain.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstractions
{
	public interface ITourGuideRepository: IBaseRepository<ApplicationUser>
	{
		//IEnumerable<ApplicationUser> GetAll(DateTime from, DateTime to);
		Task<bool> AddSelectTourGuide(string touristId, string tourGuideId, DateTime tourTime, string timeToCast, int adults);
		Task<ApplicationUser?> ToggleUserStatusAsync(string id);

	}
}
