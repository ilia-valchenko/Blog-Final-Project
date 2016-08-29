using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ORM.Models
{
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

        [Required(ErrorMessage = "Title is a required field.")]
        [MinLength(1, ErrorMessage = "Length of a title can't be empty.")]
        [MaxLength(300, ErrorMessage = "Length of a title must be less than 300 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is a required field.")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        public DateTime PublishDate { get; set; }

        //public int UserId { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
    }
}
