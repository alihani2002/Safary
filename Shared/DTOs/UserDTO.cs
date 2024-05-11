using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
	public class UserDTO
	{
		public string Message { get; set; }
		public bool IsAuthenticated { get; set; }
        public bool AdminAccepted { get; set; }
		[MaxLength(50, ErrorMessage = "Username must not exceed {0} characters")]
		public string UserName { get; set; }
		[DataType(DataType.EmailAddress, ErrorMessage = "Invalid email address.")]
		public string Email { get; set; }
        public string Address { get; set; }
        public List<string> Roles { get; set; }
		public string Token { get; set; }
		public DateTime ExpiredOn { get; set; }
	}
}
