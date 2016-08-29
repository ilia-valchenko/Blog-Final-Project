using System.Collections.Generic;

namespace BLL.Interfacies.Entities
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        public string Password { get; set; }
        public byte[] Avatar { get; set; }
    }
}

