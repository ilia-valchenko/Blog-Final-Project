using System;

namespace BLL.Interfacies.Entities
{
    public class CommentEntity
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime PublishDate { get; set; }

        public int PostId { get; set; }
        public int UserId { get; set; }
    }
}


