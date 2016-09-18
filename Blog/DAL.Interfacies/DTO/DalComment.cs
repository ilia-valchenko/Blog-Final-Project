using System;

namespace DAL.Interfacies.DTO
{
    /// <summary>
    /// This class represents a like on Data Access Layer.
    /// </summary>
    public class DalComment : IEntity
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime PublishDate { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
    }
}

