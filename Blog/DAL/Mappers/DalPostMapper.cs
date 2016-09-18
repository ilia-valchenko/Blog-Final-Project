using System;
using DAL.Interfacies.DTO;
using ORM.Models;

namespace DAL.Mappers
{
    /// <summary>
    /// This static class provides extension methods for DAL post entity.
    /// </summary>
    public static class DalPostMapper
    {
        /// <summary>
        /// This method maps ORM post to DAL post.
        /// </summary>
        /// <param name="ormPost">ORM post.</param>
        /// <returns>DAL post.</returns>
        public static DalPost ToDalPost(this Post ormPost)
        {
            if (ormPost == null)
                return null;

            return new DalPost
            {
                Id = ormPost.PostId,
                Title = ormPost.Title,
                Description = ormPost.Description,
                PublishDate = ormPost.PublishDate,
                UserId = ormPost.User.UserId
            };
        }

        /// <summary>
        /// This method maps DAL post to ORM post.
        /// </summary>
        /// <param name="dalPost">DAL post.</param>
        /// <returns>ORM post.</returns>
        public static Post ToOrmPost(this DalPost dalPost)
        {
            if (dalPost == null)
                throw new ArgumentNullException(nameof(dalPost));

            return new Post
            {
                Title = dalPost.Title,
                Description = dalPost.Description,
                PublishDate = dalPost.PublishDate
            };
        }
    }
}
