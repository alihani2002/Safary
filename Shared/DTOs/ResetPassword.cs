using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
	public class ResetPassword
	{
		[DataType(DataType.Password, ErrorMessage = "Password should have atleast one lowercase | atleast one uppercase, should have atleast one number, should have atleast one special character")]
		public string Password { get; set; }
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "The Passward and Confirm Passward do not match")]
		[Display(Name = "Confirm Passward")]
		public string ConfirmPassword { get; set; }
		[DataType(DataType.EmailAddress, ErrorMessage = "Invalid email address.")]
		public string Email { get; set; }
		public string Token { get; set; }
	}
}
