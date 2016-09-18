using System;

namespace DAL.Interfacies.DTO
{
    /// <summary>
    /// This class represents a post on Data Access Layer.
    /// </summary>
    public class DalPost : IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublishDate { get; set; }
        public int UserId { get; set; }
    }
}

