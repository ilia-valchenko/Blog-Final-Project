using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MvcPL.Models.Post
{
    public class CreatePostViewModel
    {
        [Required(ErrorMessage = "Title is a required field!")]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is a required field!")]
        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Tags")]
        public string SelectedTag { get; set; }
        public SelectList TagList { get; set; }
        public int UserId { get; set; }
    }
}