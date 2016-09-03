using BLL.Interfacies.Entities;
using DAL.Interfacies.DTO;

namespace BLL.Mappers
{
    public static class BllCommentMapper
    {
        public static DalComment ToDalComment(this CommentEntity bllComment)
        {
            if (bllComment == null)
                return null;

            return new DalComment
            {
                Id = bllComment.Id,
                Text = bllComment.Text,
                PublishDate = bllComment.PublishDate,
                //PostId = bllComment.PostId,
                //UserId = bllComment.UserId
                // Add new
                PostId = bllComment.Post.Id,
                UserId = bllComment.User.Id
            };
        }

        public static CommentEntity ToBllComment(this DalComment dalComment)
        {
            if (dalComment == null)
                return null;

            return new CommentEntity
            {
                Id = dalComment.Id,
                Text = dalComment.Text,
                PublishDate = dalComment.PublishDate,
                //PostId = dalComment.PostId,
                //UserId = dalComment.UserId
            };
        }
    }
}
