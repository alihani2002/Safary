using Shared.DTOs;

namespace Service.Abstractions
{
	public interface IAuthService
	{
		Task<UserDTO> RegisterAsUserAsync(RegisterDTO model);
	}
}
