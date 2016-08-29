using System;

namespace DAL.Interfacies.DTO
{
    public class DalComment : IEntity
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime PublishDate { get; set; }

        //public virtual DalPost Post { get; set; }
        public int PostId { get; set; }
        //public virtual DalUser User { get; set; }
        public int UserId { get; set; }
    }
}

