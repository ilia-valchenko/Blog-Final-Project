using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ORM.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }

        [Required(ErrorMessage = "Comment text can't be empty.")]
        [MinLength(1, ErrorMessage = "Comment text can't be empty.")]
        [MaxLength(300, ErrorMessage = "Length of a comment text must be less than 300 characters.")]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }

        [Required]
        public DateTime PublishDate { get; set; }

        public virtual Post Post { get; set; }

        public virtual User User { get; set; }
    }
}
