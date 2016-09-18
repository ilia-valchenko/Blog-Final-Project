using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ORM.Models
{
    /// <summary>
    /// This ORM entity represents a user which stores in the database.
    /// </summary>
    public class User
    {
        public User()
        {
            Roles = new HashSet<Role>();
            Posts = new HashSet<Post>();
            Comments = new HashSet<Comment>();
            Likes = new HashSet<Like>();
        }

        [Key]
        public int UserId { get; set; }

        [Required]
        [MaxLength(30), MinLength(3)]
        [RegularExpression(@"[a-zA-Z][a-zA-Z0-9]{3,11}$")]
        public string Nickname { get; set; }

        [Required]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}")]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public byte[] Avatar { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
    }
}
