using System.Collections.Generic;

namespace DAL.Interfacies.DTO
{
    public class DalUser : IEntity
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        public string Password { get; set; }
        public byte[] Avatar { get; set; }

        //public virtual ICollection<DalRole> Role { get; set; }
    }
}
