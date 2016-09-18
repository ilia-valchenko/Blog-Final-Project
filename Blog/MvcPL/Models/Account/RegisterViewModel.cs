using System.ComponentModel.DataAnnotations;
using System.Web;

namespace MvcPL.Models.Account
{
    public class RegisterViewModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Display(Name = "Enter your nickname")]
        [Required(ErrorMessage = "The field can not be empty!")]
        [RegularExpression(@"[a-zA-Z][a-zA-Z0-9]{3,11}$", ErrorMessage = "Nickname must contains only only letter and digits! Must starts from letter!")]
        public string Nickname { get; set; }

        [Display(Name = "Enter your email")]
        [Required(ErrorMessage = "Email field can't be empty!")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Invalid email address!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password field can't be empty!")]
        [StringLength(30, ErrorMessage = "The password must contain at least 6 characters", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Enter your password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm password field can't be empty!")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm the password")]
        [Compare("Password", ErrorMessage = "Passwords must match!")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Field for captcha code can't be empty!")]
        [Display(Name = "Enter the code from the image")]
        public string Captcha { get; set; }
    }
}