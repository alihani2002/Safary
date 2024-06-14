using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Persistence.Data;
using Persistence.Repositories;
using Service.Abstractions;

namespace Safary.Repository
{
	public class AdminRepository : BaseRepository<ApplicationUser>, IAdminRepository
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly ApplicationDbContext _context;

		public AdminRepository(UserManager<ApplicationUser> userManager, ApplicationDbContext context) : base(context)
		{
			_userManager = userManager;
			_context = context;
		}

		public async Task<string> AcceptTourGuideAsync(string email)
		{
			var user = await _userManager.FindByEmailAsync(email);
			if (user is null)
				return "Invalid User Email";
			if (user.AdminAccepted)
				return "This user already Accepted";
			user.AdminAccepted = true;
			user.EmailConfirmed = true;
			await _userManager.UpdateAsync(user);
			return "Accepted Successfully";
		}
	}
}
