using System.Collections.Generic;

namespace BLL.Interfacies.Entities
{
    /// <summary>
    /// This class represents a user on Business Logic Layer.
    /// </summary>
    public class UserEntity
    {
        public UserEntity()
        {
            Roles = new List<RoleEntity>();
        }

        public int Id { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public byte[] Avatar { get; set; }
        public List<RoleEntity> Roles { get; set; }
    }
}

