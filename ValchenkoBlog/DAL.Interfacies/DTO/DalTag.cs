using System.Collections.Generic;

namespace DAL.Interfacies.DTO
{
    public class DalTag : IEntity
    {
        //public DalTag()
        //{
        //    Posts = new HashSet<DalPost>();
        //}

        public int Id { get; set; }
        public string Name { get; set; }

        //public virtual ICollection<DalPost> Posts { get; set; }
        //public int PostId { get; set; }
    }
}

