using System;
using System.Collections.Generic;

namespace DAL.Interfacies.DTO
{
    public class DalPost : IEntity
    {
        // Add new ctor
        /*public DalPost()
        {
            Comments = new HashSet<DalComment>();
            Tags = new HashSet<DalTag>();
            Likes = new HashSet<DalLike>();
        }*/

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublishDate { get; set; }

        // Add
        public int UserId { get; set; }

        // Add new
        /*public DalUser User { get; set; }
        public ICollection<DalTag> Tags { get; set; }
        public ICollection<DalComment> Comments { get; set; }
        public ICollection<DalLike> Likes { get; set; }*/
    }
}

