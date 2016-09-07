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
                UserId = bllComment.User.Id,
                PostId = bllComment.Post.Id
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
                User = new UserEntity { Id = dalComment.UserId },
                Post = new PostEntity { Id = dalComment.PostId }
            };
        }
    }
}
