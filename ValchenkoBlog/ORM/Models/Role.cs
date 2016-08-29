using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ORM.Models
{
    public class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        [Key]
        public int RoleId { get; set; }

        /*[Required(ErrorMessage = "A name of a role is a required field.")]
        [MinLength(3, ErrorMessage = "Length of a name of a role must be greater than 3 characters.")]
        [MaxLength(30, ErrorMessage = "Length of a name of a role must be less than 30 characters.")]
        [Index(IsUnique = true)]*/
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
