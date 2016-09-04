using System.ComponentModel.DataAnnotations;

namespace MvcPL.Models.Account
{
    public class LoginViewModel
    {
        /*[Required(ErrorMessage = "The field can not be empty!")]
        [Display(Name = "Enter your nickname")]
        public string Nickname { get; set; }*/
        [Required(ErrorMessage = "The field can not be empty!")]
        [Display(Name = "Enter your email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The field can not be empty!")]
        [DataType(DataType.Password)]
        [Display(Name = "Enter your password")]
        public string Password { get; set; }

        [Display(Name = "Remember password?")]
        public bool RememberMe { get; set; }
    }
}