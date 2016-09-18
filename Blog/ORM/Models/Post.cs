using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ORM.Models
{
    /// <summary>
    /// This ORM entity represents a post which stores in the database.
    /// </summary>
    public class Post
    {
        public Post()
        {
            Comments = new HashSet<Comment>();
            Tags = new HashSet<Tag>();
            Likes = new HashSet<Like>();
        }

        [Key]
        public int PostId { get; set; }

        [Required]
        [MaxLength(200), MinLength(1)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime PublishDate { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
    }
}
