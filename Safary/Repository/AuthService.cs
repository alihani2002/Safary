using AutoMapper;
using Domain.Consts;
using Domain.Entities;
using Domain.Helpers;
using Domain.Models;
using Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Service.Abstractions;
using Shared.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Safary.Repository
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private JWT _jwt;
        private IMapper _mapper;

        private List<string> _allowedFileExtensions = new() { ".pdf", ".docx" };
        private int _maxAllowedSizeFile = 5242880;

		private List<string> _allowedImageExtensions = new() { ".jpg", ".jpeg", ".png" };
		private int _maxAllowedSizeImage = 2097152;

		public AuthService(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IUnitOfWork unitOfWork, IOptions<JWT> jwt,
            IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
            _jwt = jwt.Value;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<TourGuideDTO> RegisterAsTourGuideAsync(RegisterTourGuideDTO model)
        {
            if (await _userManager.FindByEmailAsync(model.Email) != null)
                return new TourGuideDTO { Message = "Email is already registed!" };

            if (await _userManager.FindByNameAsync(model.UserName) != null)
                return new TourGuideDTO { Message = "UserName is already registed!" };

            var tourGuide = _mapper.Map<ApplicationUser>(model);

            var extension = Path.GetExtension(model.CV.FileName);

            if (!_allowedFileExtensions.Contains(extension))
                return new TourGuideDTO { Message = "Only .pdf, .docx files are allowed!" };

            if (model.CV.Length > _maxAllowedSizeFile)
                return new TourGuideDTO { Message = "File cannot be more than 5 MB!" };

            var fileName = $"{Guid.NewGuid()}{extension}";

            var path = Path.Combine($"{_webHostEnvironment.WebRootPath}/pdfs", fileName);
            using var stream = File.Create(path);
            model.CV.CopyTo(stream);

            tourGuide.CvUrl = fileName;

			//var extensionImage = Path.GetExtension(model.Image.FileName);
			//if (!_allowedImageExtensions.Contains(extensionImage))
			//	return new TourGuideDTO { Message = "Only .jpg, .jpeg, .png images are allowed!" };
			//if (model.Image.Length > _maxAllowedSizeImage)
			//	return new TourGuideDTO { Message = "Image cannot be more than 2 MB!" };
			//var imageName = $"{Guid.NewGuid()}{extensionImage}";
			//var pathImage = Path.Combine($"{_webHostEnvironment.WebRootPath}/images/tourguides", imageName);
			//using var streamImage = File.Create(pathImage);
			//model.Image.CopyTo(streamImage);
			//tourGuide.ImageUrl = imageName;

			var result = await _userManager.CreateAsync(tourGuide, model.Password);
            if (!result.Succeeded)
            {
                var errors = string.Empty;
                foreach (var item in result.Errors)
                {
                    errors += $"{item.Description},";
                }
                return new TourGuideDTO { Message = errors };
            }
            await _userManager.AddToRoleAsync(tourGuide, AppRoles.TourGuide);

            var JwtSecurityToken = await CreateJwtToken(tourGuide);
            var returnModel = _mapper.Map<TourGuideDTO>(tourGuide);
            returnModel.ExpiredOn = JwtSecurityToken.ValidTo;
            returnModel.IsAuthenticated = true;
            returnModel.Roles = [AppRoles.TourGuide];
            returnModel.Token = new JwtSecurityTokenHandler().WriteToken(JwtSecurityToken);
            return returnModel;
        }

        public async Task<UserDTO> RegisterAsUserAsync(RegisterDTO model)
        {
            if (await _userManager.FindByEmailAsync(model.Email) != null)
                return new UserDTO { Message = "Email is already registed!" };

            if (await _userManager.FindByNameAsync(model.UserName) != null)
                return new UserDTO { Message = "UserName is already registed!" };

            var user = _mapper.Map<ApplicationUser>(model);

            //var extensionImage = Path.GetExtension(model.Image?.FileName);

            //if (!_allowedImageExtensions.Contains(extensionImage))
            //    return new TourGuideDTO { Message = "Only .jpg, .jpeg, .png images are allowed!" };

            //if (model.Image.Length > _maxAllowedSizeImage)
            //    return new TourGuideDTO { Message = "Image cannot be more than 2 MB!" };

            //var imageName = $"{Guid.NewGuid()}{extensionImage}";

            //var pathImage = Path.Combine($"{_webHostEnvironment.WebRootPath}/images/tourists", imageName);
            //using var streamImage = File.Create(pathImage);
            //model.Image.CopyTo(streamImage);

            //user.ImageUrl = imageName;

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                var errors = string.Empty;
                foreach (var item in result.Errors)
                {
                    errors += $"{item.Description},";
                }
                return new UserDTO { Message = errors };
            }
            await _userManager.AddToRoleAsync(user, AppRoles.User);

            var JwtSecurityToken = await CreateJwtToken(user);
            var returnModel = _mapper.Map<UserDTO>(user);
            returnModel.ExpiredOn = JwtSecurityToken.ValidTo;
            returnModel.IsAuthenticated = true;
            returnModel.Roles = [AppRoles.User];
            returnModel.Token = new JwtSecurityTokenHandler().WriteToken(JwtSecurityToken);
            return returnModel;
        }

		public async Task<UserDTO> RegisterAsAdminAsync(RegisterDTO model)
		{
			if (await _userManager.FindByEmailAsync(model.Email) != null)
				return new UserDTO { Message = "Email is already registed!" };

			if (await _userManager.FindByNameAsync(model.UserName) != null)
				return new UserDTO { Message = "UserName is already registed!" };

			var admin = _mapper.Map<ApplicationUser>(model);           

            var result = await _userManager.CreateAsync(admin, model.Password);
			if (!result.Succeeded)
			{
				var errors = string.Empty;
				foreach (var item in result.Errors)
				{
					errors += $"{item.Description},";
				}
				return new UserDTO { Message = errors };
			}
			await _userManager.AddToRoleAsync(admin, AppRoles.Admin);

			var JwtSecurityToken = await CreateJwtToken(admin);
			var returnModel = _mapper.Map<UserDTO>(admin);
			returnModel.ExpiredOn = JwtSecurityToken.ValidTo;
			returnModel.IsAuthenticated = true;
			returnModel.Roles = [AppRoles.Admin];
			returnModel.Token = new JwtSecurityTokenHandler().WriteToken(JwtSecurityToken);
			return returnModel;
		}

		private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();
            foreach (var role in roles)
                roleClaims.Add(new Claim("Role", role));

			var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
                new Claim(JwtRegisteredClaimNames.Name, user.LastName),
			
				new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.ValidIssuer,
                audience: _jwt.ValidAudiance,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }

		// Login
		public async Task<AuthModel> GetTokenAsync(LoginDTO model)
		{
			var authModel = new AuthModel();
			var user = await _userManager.Users
				.FirstOrDefaultAsync(x => x.Email == model.Email);
           if (user is null || !await _userManager.CheckPasswordAsync(user, model.Password))
		   {
					authModel.Message = "Email or Password is incorrect!";
					return new AuthModel { Message = authModel.Message };
		   }
            if(await _userManager.IsInRoleAsync(user, "User"))
            {				
				if (!user.EmailConfirmed)
				{
					authModel.Message = "Your Email Not Verified!";
					return new AuthModel { Message = authModel.Message };
				}
				return await CreateTokenAsync(authModel, user);
			}
			if (!user.AdminAccepted)
			{
				authModel.Message = "Please wait to accept from admin!";
				return new AuthModel { Message = authModel.Message };
			}
			if (!user.EmailConfirmed)
			{
				authModel.Message = "Your Email Not Verified!";
				return new AuthModel { Message = authModel.Message };
			}
			

            return await CreateTokenAsync(authModel, user);
		}

        public async Task<AuthModel> CreateTokenAsync(AuthModel authModel, ApplicationUser user)
        {
			authModel = _mapper.Map<AuthModel>(user);
			var jwtSecurityToken = await CreateJwtToken(user);
			authModel.IsAuthenticated = true;
			authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
			authModel.ExpiredOn = jwtSecurityToken.ValidTo;
			authModel.Roles = (List<string>)await _userManager.GetRolesAsync(user);
            return authModel;
		} 

		//public async Task<AuthModel> GetTokenAsUserAsync(LoginDTO model)
		//{
		//	var authModel = new AuthModel();
		//	var user = await _userManager.Users
		//		.FirstOrDefaultAsync(x => x.Email == model.Email);

		//	if (user is null || !await _userManager.CheckPasswordAsync(user, model.Password))
		//	{
		//		authModel.Message = "Email or Password is incorrect!";
		//		return new AuthModel { Message = authModel.Message };
		//	}
		//	if (!user.EmailConfirmed)
		//	{
		//		authModel.Message = "Your Email Not Verified!";
		//		return new AuthModel { Message = authModel.Message };
		//	}
		//	if (!user.AdminAccepted)
		//	{
		//		authModel.Message = "Please wait to accept from admin!";
		//		return new AuthModel { Message = authModel.Message };
		//	}

		//	authModel = _mapper.Map<AuthModel>(user);
		//	var jwtSecurityToken = await CreateJwtToken(user);
		//	authModel.IsAuthenticated = true;
		//	authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
		//	authModel.ExpiredOn = jwtSecurityToken.ValidTo;
		//	authModel.Roles = (List<string>)await _userManager.GetRolesAsync(user);
		//	return authModel;
		//}
		// Confirm Email
		public async Task<bool> ConfirmEmailAsync(string email, string token)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is not null)
            {
                user.EmailConfirmed = true;
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                    return true;
            }
            return false;
        }

	}
}
