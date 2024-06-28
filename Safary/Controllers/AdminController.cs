using AutoMapper;
using Domain.Consts;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Safary.Repository;
using Service.Abstractions;
using Shared.DTOs;

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
		private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(IAdminRepository adminRepository, IEmailSender emailSender,
			UserManager<ApplicationUser> userManager, IEmailBodyBuilder emailBodyBuilder, IMapper mapper, RoleManager<IdentityRole> roleManager)
        {
            _adminRepository = adminRepository;
            _emailSender = emailSender;
            _userManager = userManager;
            _emailBodyBuilder = emailBodyBuilder;
            _mapper = mapper;
            _roleManager = roleManager;
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

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
            var adminRole = await _roleManager.FindByNameAsync(AppRoles.Admin);
            if (adminRole == null)
            {
                return NotFound("Admin role not found.");
            }

            var adminUsers = await _userManager.GetUsersInRoleAsync(adminRole.Name);			

            return Ok(_mapper.Map<IEnumerable<AdminDTO>>(adminUsers));
        }

        [HttpPost("ToggleStatus/{id}")]
        public async Task<IActionResult> ToggleStatus(string id)
        {
            var user = await _adminRepository.ToggleAdminStatusAsync(id);

            return user is null ? NotFound() : Ok();
        }        
    }
}
