using System;
using MvcPL.Models.User;

namespace MvcPL.Models.Comment
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime PublishDate { get; set; }
        public UserViewModel User { get; set; }
    }
}