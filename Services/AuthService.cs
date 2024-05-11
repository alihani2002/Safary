using AutoMapper;
using Domain.Consts;
using Domain.Entities;
using Domain.Helpers;
using Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Service.Abstractions;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
	public class AuthService : IAuthService
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly IUnitOfWork _unitOfWork;
		private JWT _jwt;
		private IMapper _mapper;

		public AuthService(UserManager<ApplicationUser> userManager, 
			RoleManager<IdentityRole> roleManager,
			IUnitOfWork unitOfWork, IOptions<JWT> jwt,
			IMapper mapper)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_unitOfWork = unitOfWork;
			_jwt = jwt.Value;
			_mapper = mapper;
		}

		public async Task<UserDTO> RegisterAsUserAsync(RegisterDTO model)
		{
			if (await _userManager.FindByEmailAsync(model.Email) != null)
				return new UserDTO { Message = "Email is already registed!" };

			if (await _userManager.FindByNameAsync(model.UserName) != null)
				return new UserDTO { Message = "UserName is already registed!" };

			var user = _mapper.Map<ApplicationUser>(model);

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

		private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
		{
			var userClaims = await _userManager.GetClaimsAsync(user);
			var roles = await _userManager.GetRolesAsync(user);
			var roleClaims = new List<Claim>();
			foreach (var role in roles)
				roleClaims.Add(new Claim("roles", role));

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
	}
}
