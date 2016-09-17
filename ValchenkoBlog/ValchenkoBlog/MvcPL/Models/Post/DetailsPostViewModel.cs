using System;
using System.Collections.Generic;
using MvcPL.Models.Tag;
using MvcPL.Models.User;
using MvcPL.Models.Comment;

namespace MvcPL.Models.Post
{
    public class DetailsPostViewModel
    {
        public DetailsPostViewModel()
        {
            Tags = new List<TagViewModel>();
            Comments = new List<CommentViewModel>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string PublishDate { get; set; }
        public UserProfileViewModel Author { get; set; }
        public List<TagViewModel> Tags { get; set; }
        public List<CommentViewModel> Comments { get; set; }
        public int NumberOfLikes { get; set; }
        public bool IsLiked { get; set; }
    }
}