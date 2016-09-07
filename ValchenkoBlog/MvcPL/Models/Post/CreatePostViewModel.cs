using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

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
        [Display(Name = "Tags")]
        public string SelectedTag { get; set; }
        public SelectList TagList { get; set; }
        // Take it from identity in controller
        public int UserId { get; set; }
    }
}