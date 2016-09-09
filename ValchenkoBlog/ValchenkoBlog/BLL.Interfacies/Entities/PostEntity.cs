using System;
using System.Collections.Generic;

namespace BLL.Interfacies.Entities
{
    public class PostEntity
    {
        public PostEntity()
        {
            User = new UserEntity();
            Tags = new List<TagEntity>();
            Comments = new List<CommentEntity>();
            Likes = new List<LikeEntity>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublishDate { get; set; }
        public UserEntity User { get; set; }
        public List<TagEntity> Tags { get; set; }
        public List<CommentEntity> Comments { get; set; }
        public List<LikeEntity> Likes { get; set; }
     }
}
