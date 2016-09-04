using System.Collections.Generic;

namespace DAL.Interfacies.DTO
{
    public class DalUser : IEntity
    {
        // Add new ctor
        /*public DalUser()
        {
            Roles = new HashSet<DalRole>();
            Posts = new HashSet<DalPost>();
            Likes = new HashSet<DalLike>();
        }*/

        public int Id { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public byte[] Avatar { get; set; }

        // Add new 

        /*public ICollection<DalRole> Roles { get; set; }
        public ICollection<DalPost> Posts { get; set; }
        public ICollection<DalComment> Comments { get; set; }
        public ICollection<DalLike> Likes { get; set; }*/
      }
}
