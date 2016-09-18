using System.ComponentModel.DataAnnotations;

namespace MvcPL.Models.Tag
{
    public class CreateTagViewModel
    {
        [Required]
        [MaxLength(30), MinLength(1)]
        public string Name { get; set; }
    }
}