using System;
using System.Collections.Generic;
using MvcPL.Models.Tag;
using MvcPL.Models.User;

namespace MvcPL.Models.Post
{
    public class PostViewModel
    {
        public PostViewModel()
        {
            PublishDate = DateTime.Now.ToShortDateString();
            Tags = new List<TagViewModel>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string PublishDate { get; set; }

        public UserViewModel Author { get; set; }

        public List<TagViewModel> Tags { get; set; }
        public int NumberOfLikes { get; set; }
        public int NumberOfComments { get; set; }
    }
}

