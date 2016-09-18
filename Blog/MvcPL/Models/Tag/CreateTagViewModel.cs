using System.ComponentModel.DataAnnotations;

namespace MvcPL.Models.Tag
{
    public class CreateTagViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}