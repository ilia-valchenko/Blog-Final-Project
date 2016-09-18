using System.ComponentModel.DataAnnotations;

namespace MvcPL.Models.Tag
{
    public class EditTagViewModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        [Required]
        [MaxLength(30), MinLength(1)]
        public string Name { get; set; }
    }
}