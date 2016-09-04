using System;
using System.ComponentModel.DataAnnotations;

namespace MvcPL.Models.Account
{
    public class RegisterViewModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Display(Name = "Enter your nickname")]
        [Required(ErrorMessage = "The field can not be empty!")]
        //[RegularExpression(@"(\w+)", ErrorMessage = "Invalid name")]
        [StringLength(30, ErrorMessage = "The name must contain at lest {2} characters", MinimumLength = 4)]
        public string Nickname { get; set; }

        [Display(Name = "Enter your email")]
        [Required(ErrorMessage = "The field can not be empty!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter your password")]
        [StringLength(100, ErrorMessage = "The password must contain at least {2} characters", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Enter your password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm the password")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm the password")]
        [Compare("Password", ErrorMessage = "Passwords must match")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Enter the code from the image")]
        public string Captcha { get; set; }
    }
}