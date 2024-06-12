using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Service.Abstractions;
using Shared.DTOs;

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

    public AccountController(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork, IAuthService authService, IEmailSender emailSender, IEmailBodyBuilder emailBodyBuilder)
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;
        _authService = authService;
        _emailSender = emailSender;
        _emailBodyBuilder = emailBodyBuilder;
    }

    [HttpPost("Register-As-User")]
    public async Task<IActionResult> RegisterAsUserAsync([FromForm] RegisterDTO model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var result = await _authService.RegisterAsUserAsync(model);

        if (!result.IsAuthenticated)
            return BadRequest(result.Message);
        var user = await _userManager.FindByEmailAsync(result.Email);
        if (user == null)
            return BadRequest(ModelState);
        //// Confirm email
        await ConfirmAndSendEmailAsync(user);
        result.Message = "Please Look in your email box";
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
