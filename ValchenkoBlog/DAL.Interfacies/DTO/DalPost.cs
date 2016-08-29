using System;
using System.Collections.Generic;

namespace DAL.Interfacies.DTO
{
    public class DalPost : IEntity
    {
        //public DalPost()
        //{
        //    Comments = new HashSet<DalComment>();
        //    Tags = new HashSet<DalTag>();
        //    Likes = new HashSet<DalLike>();
        //}

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublishDate { get; set; }

        //public virtual DalUser User { get; set; }
        public int UserId { get; set; }

        //public virtual ICollection<DalTag> Tags { get; set; }
        //public virtual ICollection<DalComment> Comments { get; set; }
        //public virtual ICollection<DalLike> Likes { get; set; }
    }
}

