using System.Collections.Generic;

namespace MvcPL.Models.User
{
    public class UserProfileViewModel
    {
        public UserProfileViewModel()
        {
            Roles = new List<string>();
        }

        public int Id { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }
        public byte[] Avatar { get; set; }
        public List<string> Roles { get; set; }
    }
}