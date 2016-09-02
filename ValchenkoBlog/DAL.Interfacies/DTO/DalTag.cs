using System.Collections.Generic;

namespace DAL.Interfacies.DTO
{
    public class DalTag : IEntity
    {
        // Add new ctor
        /*public DalTag()
        {
            Posts = new HashSet<DalPost>();
        }*/

        public int Id { get; set; }
        public string Name { get; set; }

        // Add new
        //public ICollection<DalPost> Posts { get; set; }
    }
}

