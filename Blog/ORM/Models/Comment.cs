using System;
using System.ComponentModel.DataAnnotations;

namespace ORM.Models
{
    /// <summary>
    /// This ORM entity represents a like which stores in the database.
    /// </summary>
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }

        [Required]
        [MaxLength(300), MinLength(1)]
        public string Text { get; set; }

        [Required]
        public DateTime PublishDate { get; set; }

        public virtual Post Post { get; set; }

        public virtual User User { get; set; }
    }
}
