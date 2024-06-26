using Domain.Consts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
	public class RegisterDTO
	{
		[MaxLength(50, ErrorMessage = Errors.MaxLength)]
		public string FirstName { get; set; }
		[MaxLength(50, ErrorMessage = Errors.MaxLength)]
		public string LastName { get; set; }
		[MaxLength(100, ErrorMessage = Errors.MaxLength)]
		public string FullName { get; set; }
        [MaxLength(100, ErrorMessage = Errors.MaxLength)]
        public string UserName { get; set; }
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }

		[DataType(DataType.Password, ErrorMessage = Errors.WeakPassword)]
		public string Password { get; set; }
		[Compare("Password", ErrorMessage = Errors.ConfirmPasswordNotMatch)]
		[Display(Name = "Confirm Passward"), DataType(DataType.Password)]
		public string ConfirmPassword { get; set; }
        public string Address { get; set; }
		[RegularExpression(RegexPatterns.MobileNumber, ErrorMessage = Errors.RegexPhoneNumber)]
		public string PhoneNumber { get; set; }
        public IFormFile Image { get; set; } = null!;


    }
}
