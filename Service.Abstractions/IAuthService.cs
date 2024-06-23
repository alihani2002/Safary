using Domain.Models;
using Shared.DTOs;

namespace Service.Abstractions
{
	public interface IAuthService
	{
		Task<UserDTO> RegisterAsUserAsync(RegisterDTO model);
		Task<UserDTO> RegisterAsAdminAsync(RegisterDTO model);
		Task<TourGuideDTO> RegisterAsTourGuideAsync(RegisterTourGuideDTO model);
        Task<bool> ConfirmEmailAsync(string email, string token);
		//Task<AuthModel> GetTokensTourGuideAsync(LoginDTO model);
		Task<AuthModel> GetTokenAsync(LoginDTO model);
	}
}
