using System;

namespace DAL.Interfacies.DTO
{
    public class DalComment : IEntity
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public DateTime PublishDate { get; set; }

        // Add
        public int UserId { get; set; }
        public int PostId { get; set; }
        // Add new
        /*public DalPost Post { get; set; }
        public DalUser User { get; set; }*/
    }
}

