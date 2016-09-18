using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ORM.Models
{
    /// <summary>
    /// This ORM entity represents a role which stores in the database.
    /// </summary>
    public class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        [Key]
        public int RoleId { get; set; }

        [Required]
        [MaxLength(15), MinLength(3)]
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
