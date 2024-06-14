using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Service.Abstractions;

namespace Safary.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AdminController : ControllerBase
	{
		private readonly IAdminRepository _adminRepository;
		private readonly IEmailSender _emailSender;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IEmailBodyBuilder _emailBodyBuilder;
		private readonly IMapper _mapper;

		public AdminController(IAdminRepository adminRepository, IEmailSender emailSender, UserManager<ApplicationUser> userManager, IEmailBodyBuilder emailBodyBuilder, IMapper mapper)
		{
			_adminRepository = adminRepository;
			_emailSender = emailSender;
			_userManager = userManager;
			_emailBodyBuilder = emailBodyBuilder;
			_mapper = mapper;
		}

		[HttpPost("Accept-TourGuide")]
		public async Task<IActionResult> AcceptTourGuideAsync(string UserEmail)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var result = await _adminRepository.AcceptTourGuideAsync(UserEmail);

			var user = await _userManager.FindByEmailAsync(UserEmail);
			if (user == null)
				return BadRequest(ModelState);

			// Send welcome email to user
			var Placeholders = new Dictionary<string, string>()
					 {
						 {"imageUrl","https://res.cloudinary.com/mhmdnosair/image/upload/v1700498236/icon-positive-vote-2_sgatwf.png"},
						 {"header",$"Hey {user.FirstName}, thanks for joining us!" },
						 {"body","Your account has been successfully reviewed, Welcome for Joining Safary" },
					 };
			#region Send massage Email
			var body = _emailBodyBuilder.GetEmailBody("notification", Placeholders);
			await _emailSender.SendEmailAsync(user.Email!, "Welcome to Safary", body);
			#endregion

			return Ok("Accepted Successfully");
		}
	}
}
