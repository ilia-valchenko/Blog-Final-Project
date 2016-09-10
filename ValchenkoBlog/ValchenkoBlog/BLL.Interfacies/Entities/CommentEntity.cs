using System;

namespace BLL.Interfacies.Entities
{
    public class CommentEntity
    {
        public CommentEntity()
        {
            Post = new PostEntity();
            User = new UserEntity();
        }

        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime PublishDate { get; set; }
        //public int PostId { get; set; }
        //public int UserId { get; set; }
        // Add new
        public PostEntity Post { get; set; }
        public UserEntity User { get; set; }
    }
}


