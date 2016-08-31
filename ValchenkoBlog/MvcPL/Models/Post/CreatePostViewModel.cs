using System.ComponentModel.DataAnnotations;

namespace MvcPL.Models.Post
{
    public class CreatePostViewModel
    {
        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        // Tags
    }
}