using System.ComponentModel.DataAnnotations;

namespace MvcPL.Models.User
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        public byte[] Avatar { get; set; }
    }
}