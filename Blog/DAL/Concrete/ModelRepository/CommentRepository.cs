using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Interfacies.DTO;
using DAL.Interfacies.Repository.ModelRepository;
using ORM.Models;
using DAL.Mappers;
using System.Data.Entity;

namespace DAL.Concrete.ModelRepository
{
    /// <summary>
    /// Realization of ICommentRepository interface.
    /// </summary>
    public class CommentRepository : ICommentRepository
    {
        public CommentRepository(DbContext context)
        {
            this.context = context;
        }

        #region CRUD operations
        public void Create(DalComment entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var comment = entity.ToOrmComment();
            comment.User = context.Set<User>().FirstOrDefault(u => u.UserId == entity.UserId);
            comment.Post = context.Set<Post>().FirstOrDefault(p => p.PostId == entity.PostId);

            context.Set<Post>().FirstOrDefault(p => p.PostId == entity.PostId)?.Comments.Add(comment);
        }

        public void Update(DalComment entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var comment = context.Set<Comment>().FirstOrDefault(c => c.CommentId == entity.Id);

            if (comment != null)
            {
                comment.Text = entity.Text;
                comment.PublishDate = entity.PublishDate;
            }
        }

        public void Delete(DalComment entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var comment = context.Set<Comment>().FirstOrDefault(u => u.CommentId == entity.Id);

            if (comment != null)
                context.Set<Comment>().Remove(comment);
        }
        #endregion

        #region Get operations
        public DalComment GetById(int key) => context.Set<Comment>().FirstOrDefault(u => u.CommentId == key)?.ToDalComment();
        public IEnumerable<DalComment> GetAll() => context.Set<Comment>().ToList().Select(c => c.ToDalComment());
        /// <summary>
        /// This method returns all DAL comments which was posted by the user.
        /// </summary>
        /// <param name="userId">Id of the user.</param>
        /// <returns></returns>
        public IEnumerable<DalComment> GetDalCommentsByUserId(int userId) => context.Set<User>().FirstOrDefault(u => u.UserId == userId)?.Comments.ToList().Select(c => c.ToDalComment());
        /// <summary>
        /// This method returns DAL comments of the post.
        /// </summary>
        /// <param name="postId">Id of the post.</param>
        /// <returns>Returns collection of DAL comments.</returns>
        public IEnumerable<DalComment> GetDalCommentsByPostId(int postId) => context.Set<Post>().FirstOrDefault(p => p.PostId == postId)?.Comments.ToList().Select(c => c.ToDalComment());
        #endregion

        /// <summary>
        /// This method delete comments from the post.
        /// </summary>
        /// <param name="postId">Id of the post.</param>
        public void DeleteCommentsFromPost(int postId)
        {
            var comments = context.Set<Comment>().Where(comment => comment.Post.PostId == postId);

            if (comments != null)
                foreach (var comment in comments)
                    context.Set<Comment>().Remove(comment);
        }

        private readonly DbContext context;
    }
}
