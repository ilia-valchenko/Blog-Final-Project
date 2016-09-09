using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ORM.Models
{
    public class Tag
    {
        public Tag()
        {
            Posts = new HashSet<Post>();
        }

        [Key]
        public int TagId { get; set; }

        [Required(ErrorMessage = "A name of a tag is a required field.")]
        [MaxLength(30, ErrorMessage = "Length of a name of a tag must be less than 30 characters.")]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
