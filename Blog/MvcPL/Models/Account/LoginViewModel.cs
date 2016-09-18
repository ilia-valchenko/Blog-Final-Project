using System.ComponentModel.DataAnnotations;

namespace MvcPL.Models.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "The field can not be empty!")]
        [Display(Name = "Enter your login")]
        [RegularExpression(@"[a-zA-Z][a-zA-Z0-9]{3,11}$", ErrorMessage = "Nickname must contains only only letter and digits! Must starts from letter!")]
        public string Nickname { get; set; }

        [Required(ErrorMessage = "The field can not be empty!")]
        [DataType(DataType.Password)]
        [Display(Name = "Enter your password")]
        public string Password { get; set; }

        [Display(Name = "Remember password?")]
        public bool RememberMe { get; set; }
    }
}