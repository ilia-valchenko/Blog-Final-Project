using System.ComponentModel.DataAnnotations;

namespace MvcPL.Models.User
{
    public class RegisterViewModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Display(Name = "Enter your nickname")]
        [Required(ErrorMessage = "The field can not be empty!")]
        // Make something new (regex).
        [RegularExpression(@"(\w+)", ErrorMessage = "Invalid name")]
        [StringLength(30, ErrorMessage = "The name must contain at lest {2} characters", MinimumLength = 4)]
        public string Nickname { get; set; }

        [Required(ErrorMessage = "Password field can't be empty!")]
        [StringLength(100, ErrorMessage = "The password must contain at least {2} characters", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Enter your password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm password field can't be empty! Plaese try again.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm the password")]
        [Compare("Password", ErrorMessage = "Passwords must match")]
        public string ConfirmPassword { get; set; }

        // Avatar
    }
}