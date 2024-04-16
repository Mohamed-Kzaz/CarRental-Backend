using System.ComponentModel.DataAnnotations;

namespace CarRental.APIs.DTOs.Account
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "First name is required!")]
        public string FName { get; set; }

        [Required(ErrorMessage = "Last name is required!")]
        public string LName { get; set; }

        [Required(ErrorMessage = "Email is required!")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Address is required!")]
        public string Address { get; set; }


        [Required(ErrorMessage = "PhoneNumber is required!")]
        [RegularExpression(@"^01[0125][0-9]{8}$", ErrorMessage = "Phone number must be Egyptian")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }


        [Required(ErrorMessage = "Date of birth is required!")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime DOB { get; set; }

        [Required(ErrorMessage = "Driving license is required!")]
        public IFormFile DrivingLic { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Confirmation password do not match.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
