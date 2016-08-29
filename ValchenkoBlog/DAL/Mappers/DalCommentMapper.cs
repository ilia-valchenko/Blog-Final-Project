using ORM.Models;
using DAL.Interfacies.DTO;

namespace DAL.Mappers
{
    public static class DalCommentMapper
    {
        public static DalComment ToDalComment(this Comment ormComment)
        {
            if (ormComment == null)
                return null;

            return new DalComment
            {
                Id = ormComment.CommentId,
                Text = ormComment.Text,
                PublishDate = ormComment.PublishDate,
                //PostId = ormComment.PostId,
                PostId = ormComment.Post.PostId,
                //UserId = ormComment.UserId
                UserId = ormComment.User.UserId
            };
        }

        public static Comment ToOrmComment(this DalComment dalComment)
        {
            if (dalComment == null)
                return null;

            return new Comment
            {
                // without CommentId
                Text = dalComment.Text,
                PublishDate = dalComment.PublishDate,
                //PostId = dalComment.PostId,
                //UserId = dalComment.UserId
            };
        }
    }
}
