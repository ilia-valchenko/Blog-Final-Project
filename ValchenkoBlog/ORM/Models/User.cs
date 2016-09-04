using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ORM.Models
{
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

        [Required(ErrorMessage = "Nickname is a required field.")]
        /*[MaxLength(30, ErrorMessage = "Length of nickname must be less than 30 characters.")]
        [MinLength(4, ErrorMessage = "Length of nickname must be greater than 4 characters.")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Numbers and special characters are not allowed in the name.")]
        [Index(IsUnique = true)]*/
        public string Nickname { get; set; }

        [Required(ErrorMessage = "Email is a required field.")]
        // Add regex here
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is a required field.")]
        //[MaxLength(30, ErrorMessage = "Length of password must be less than 30 characters.")]
        //[MinLength(4, ErrorMessage = "Length of password must be greater than 4 characters.")]
        /*[DataType(DataType.Password)]*/
        public string Password { get; set; }

        public byte[] Avatar { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
    }
}
