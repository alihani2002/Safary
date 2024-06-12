using Shared.DTOs;

namespace Service.Abstractions
{
	public interface IAuthService
	{
		Task<UserDTO> RegisterAsUserAsync(RegisterDTO model);
		Task<TourGuideDTO> RegisterAsTourGuideAsync(RegisterTourGuideDTO model);
        Task<bool> ConfirmEmailAsync(string email, string token);

    }
}
