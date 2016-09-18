using System.ComponentModel.DataAnnotations;

namespace ORM.Models
{
    /// <summary>
    /// This ORM entity represents a like which stores in the database.
    /// </summary>
    public class Like
    {
        [Key]
        public int LikeId { get; set; }

        public virtual Post Post { get; set; }

        public virtual User User { get; set; }
    }
}
