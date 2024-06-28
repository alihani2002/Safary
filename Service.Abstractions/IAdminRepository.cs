using Domain.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstractions
{
	public interface IAdminRepository: IBaseRepository<ApplicationUser>
	{
		Task<string> AcceptTourGuideAsync(string email);
        Task<ApplicationUser?> ToggleAdminStatusAsync(string id);
    }
}
