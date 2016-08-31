using System;
using System.Collections.Generic;
using MvcPL.Models.Tag;

namespace MvcPL.Models.Post
{
    public class PostViewModel
    {
        public PostViewModel()
        {
            PublishDate = DateTime.Now.ToShortDateString();
            Tags = new HashSet<TagModel>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string PublishDate { get; set; }
        public string AuthorNickname { get; set; }

        public virtual ICollection<TagModel> Tags { get; set; }
        public int NumberOfLikes { get; set; }
    }
}
