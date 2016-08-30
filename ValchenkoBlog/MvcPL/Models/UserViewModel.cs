using System.ComponentModel.DataAnnotations;

namespace MvcPL.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Nickname")]
        public string Nickname { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}