using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Service.Abstractions;
using Shared.DTOs;
using Sieve.Models;
using Sieve.Services;

namespace Safary.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthService _authService;
    private readonly IEmailSender _emailSender;
    private readonly IEmailBodyBuilder _emailBodyBuilder;
	private readonly IUrlHelper _urlHelper;
	private readonly ILogger<AccountController> _logger;
    private readonly ISieveProcessor _sieveProcessor;
	private readonly IMapper _mapper;

    public AccountController(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork, IAuthService authService, IEmailSender emailSender, IEmailBodyBuilder emailBodyBuilder, IUrlHelper urlHelper, ILogger<AccountController> logger, ISieveProcessor sieveProcessor , IMapper mapper)
	{
		_userManager = userManager;
		_unitOfWork = unitOfWork;
		_authService = authService;
		_emailSender = emailSender;
		_emailBodyBuilder = emailBodyBuilder;
		_urlHelper = urlHelper;
		_logger = logger;
		_sieveProcessor = sieveProcessor;
		_mapper = mapper;
	}

	[HttpPost("Register-As-User")]
    public async Task<IActionResult> RegisterAsUserAsync([FromBody] RegisterDTO model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var result = await _authService.RegisterAsUserAsync(model);

        if (!result.IsAuthenticated)
            return BadRequest(result.Message);
        var user = await _userManager.FindByEmailAsync(result.Email);
        if (user == null)
            return BadRequest(ModelState);
        // Confirm email
        await ConfirmAndSendEmailAsync(user);
        result.Message = "Please Look in your email box";
        return Ok(result);
    }

	[HttpPost("Register-As-Admin")]
	public async Task<IActionResult> RegisterAsAdminAsync([FromBody] RegisterDTO model)
	{
		if (!ModelState.IsValid)
			return BadRequest(ModelState);
		var result = await _authService.RegisterAsAdminAsync(model);

		if (!result.IsAuthenticated)
			return BadRequest(result.Message);
		var admin = await _userManager.FindByEmailAsync(result.Email);
		if (admin == null)
			return BadRequest(ModelState);
		result.Message = "Welcome new admin";
		return Ok(result);
	}


	[HttpPost("Register-As-TourGuide")]
	public async Task<IActionResult> RegisterAsTourGuideAsync([FromForm] RegisterTourGuideDTO model)
	{
		if (!ModelState.IsValid)
			return BadRequest(ModelState);
		var result = await _authService.RegisterAsTourGuideAsync(model);

		if (!result.IsAuthenticated)
			return BadRequest(result.Message);

		var user = await _userManager.FindByEmailAsync(result.Email);
		if (user == null)
			return BadRequest(ModelState);

		result.Message = "added Successfully";
		return Ok(result);
	}

	[HttpPost("Login")]
	public async Task<IActionResult> LoginAsync([FromBody] LoginDTO model)
	{
		if (!ModelState.IsValid)
			return BadRequest(ModelState);

		var result = await _authService.GetTokenAsync(model);

		if (!result.IsAuthenticated)
			return BadRequest(result.Message);
		result.Message = "Success";
		return Ok(result);
	}

	[HttpGet("Confirm-Email")]
    public async Task<IActionResult> ConfirmEmail(string email, string token)
    {
        var confirm = await _authService.ConfirmEmailAsync(email, token);
        if (confirm)
            return Ok("Email Verified Successfully!" +
                "Pleaese wait admin aceept..");
        return BadRequest("This User Not Exist!");
    }
	
    [HttpGet("GetAllUsers")]
    public async Task<ActionResult> GetUsers()
    {
        var users = await _unitOfWork.ApplicationUsers.GetAll();
        var dto = _mapper.Map<IEnumerable<ApplicationUsersDTO>>(users);
        return Ok(dto);
    }
    [HttpGet("GetFilterdAndSorted")]
    public async Task<IActionResult> GetFilterdAndSorted([FromQuery] SieveModel sieveModel)
    {
        var ApplicationUsers = _unitOfWork.ApplicationUsers.FilterGetAll();

        // Apply Sieve to the queryable collection
        var filteredSortedPagedProducts = _sieveProcessor.Apply(sieveModel, ApplicationUsers);
        var dto = _mapper.Map<IEnumerable<ApplicationUsersDTO>>(filteredSortedPagedProducts);
        return Ok(dto);
    }
    // Forget Password
    [HttpPost("Forget-Passward")]
	public async Task<IActionResult> ForgetPassward(string email)
	{
		if (string.IsNullOrEmpty(email))
			return BadRequest("Invalid email address");

		var user = await _userManager.FindByEmailAsync(email);

		if (user is null)
			return BadRequest("User not found. Please try again.");

		if (!await _userManager.IsEmailConfirmedAsync(user))
			return BadRequest("Email not verified. Please verify your email address.");

		var token = await _userManager.GeneratePasswordResetTokenAsync(user);

		var passResetLink = _urlHelper.Action(nameof(ResetPassward), "Account", new { Email = email, Token = token }, Request.Scheme);

		_logger.Log(LogLevel.Warning, passResetLink);

		try
		{
			await _emailSender.SendEmailAsync(email, "Password Reset", $"Password Reset Link: {passResetLink}");
			return Ok("Password reset link sent successfully. Check your email to reset your password.");
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error sending password reset email");
			return StatusCode(500, "Error sending password reset email. Please try again later.");
		}
	}

	// Reset Password
	[HttpPost("Reset-Passward")]
	public async Task<IActionResult> ResetPassward(ResetPassword resetPassword)
	{
		if (string.IsNullOrEmpty(resetPassword.Email))
			return BadRequest("Invalid email address");

		var user = await _userManager.FindByEmailAsync(resetPassword.Email);

		if (user is null)
			return BadRequest("User not found. Please try again.");

		var resetPassResult = await _userManager.ResetPasswordAsync(user, resetPassword.Token, resetPassword.Password);

		if (!resetPassResult.Succeeded)
		{
			foreach (var error in resetPassResult.Errors)
				ModelState.AddModelError(error.Code, error.Description);

			return Ok(ModelState);
		}
		return Ok("Password has changed");
	}

	[HttpPost("logout")]
	[Authorize]
	public async Task<IActionResult> Logout()
	{
		await HttpContext.SignOutAsync();
		return Ok(new { message = "Logged out successfully" });
	}

	private async Task ConfirmAndSendEmailAsync(ApplicationUser user)
    {
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user!);
        var callbackUrl = Url.Action(
           nameof(ConfirmEmail),
           "Account",
           values: new { token, email = user!.Email },
           protocol: Request.Scheme);

        var Placeholders = new Dictionary<string, string>()
        {
            {"imageUrl","https://res.cloudinary.com/mhmdnosair/image/upload/v1700380565/icon-positive-vote-1_nvd6xb.png"},
            {"header",$"Hey {user.FirstName}, thanks for joining us!" },
            {"body","please confirm your email" },
            {"url",$"{callbackUrl}" },
            {"linkTitle","Active Acount" },
        };
        #region Send massage Email
        var body = _emailBodyBuilder.GetEmailBody("email", Placeholders);
        await _emailSender.SendEmailAsync(user.Email!, "Confirm your email", body);
        #endregion
    }


}
