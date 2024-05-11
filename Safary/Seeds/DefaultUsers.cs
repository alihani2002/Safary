using Domain.Consts;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Safary.Seeds
{
	public static class DefaultUsers
	{
		public static async Task SeedAdminUserAsync(UserManager<ApplicationUser> userManager)
		{
			if (!userManager.Users.Any())
			{
				ApplicationUser admin = new()
				{
					FirstName = "admin",
					LastName = "user",
					UserName = "admin",
					FullName = "Admin",
					Email = "admin@Safary.com",
					Address = "test address",
					EmailConfirmed = true,
					AdminAccepted = true,
				};

				var user = await userManager.FindByEmailAsync(admin.Email);
				if (user is null)
				{
					await userManager.CreateAsync(admin, "Passw@rd12345678");
					await userManager.AddToRoleAsync(admin, AppRoles.Admin);
				}
			}	
		}
	}
}
