using System.ComponentModel.DataAnnotations;

namespace MvcPL.Models.User
{
    public class UserViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Nickname")]
        public string Nickname { get; set; }

        public byte[] Avatar { get; set; }
    }
}