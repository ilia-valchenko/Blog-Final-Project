using System;
using ORM.Models;
using DAL.Interfacies.DTO;

namespace DAL.Mappers
{
    /// <summary>
    /// This static class provides extension methods for DAL comment entity.
    /// </summary>
    public static class DalCommentMapper
    {
        /// <summary>
        /// This method maps ORM comment to DAL comment.
        /// </summary>
        /// <param name="ormComment">ORM comment.</param>
        /// <returns>DAL comment.</returns>
        public static DalComment ToDalComment(this Comment ormComment)
        {
            if (ormComment == null)
                return null;

            return new DalComment
            {
                Id = ormComment.CommentId,
                Text = ormComment.Text,
                PublishDate = ormComment.PublishDate,
                PostId = ormComment.Post.PostId,
                UserId = ormComment.User.UserId
            };
        }

        /// <summary>
        /// This method maps DAL comment to ORM comment.
        /// </summary>
        /// <param name="dalComment">DAL comment.</param>
        /// <returns>ORM comment.</returns>
        public static Comment ToOrmComment(this DalComment dalComment)
        {
            if (dalComment == null)
                throw new ArgumentNullException(nameof(dalComment));

            return new Comment
            {
                Text = dalComment.Text,
                PublishDate = dalComment.PublishDate,
            };
        }
    }
}
