using System;

namespace BLL.Interfacies.Entities
{
    /// <summary>
    /// This class represents a comment on Business Logic Layer.
    /// </summary>
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
        public PostEntity Post { get; set; }
        public UserEntity User { get; set; }
    }
}


