using System;
using MvcPL.Models.User;
using MvcPL.Models.Post;

namespace MvcPL.Models.Comment
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string PublishDate { get; set; }
        public UserViewModel User { get; set; }
        public PostViewModel Post { get; set; }
    }
}