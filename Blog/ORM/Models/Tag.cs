using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ORM.Models
{
    /// <summary>
    /// This ORM entity represents a tag which stores in the database.
    /// </summary>
    public class Tag
    {
        public Tag()
        {
            Posts = new HashSet<Post>();
        }

        [Key]
        public int TagId { get; set; }

        [Required]
        [MaxLength(30), MinLength(1)]
        public string Name { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
