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
    /// Realization of IPostRepository interface.
    /// </summary>
    public class PostRepository : IPostRepository
    {
        public PostRepository(DbContext context)
        {
            this.context = context;
        }

        #region CRUD operations
        public void Create(DalPost entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            Create(entity, new List<DalTag>());
        }

        public void Create(DalPost entity, IEnumerable<DalTag> tags)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (tags == null)
                throw new ArgumentNullException(nameof(entity));

            var post = entity.ToOrmPost();

            if (post != null)
            {
                foreach (var dalTag in tags)
                {
                    var tag = context.Set<Tag>().FirstOrDefault(t => t.Name == dalTag.Name);
                    if (tag != null)
                        post.Tags.Add(tag);
                }
            }

            context.Set<User>().FirstOrDefault(user => user.UserId == entity.UserId)?.Posts.Add(post);
        }

        public void Update(DalPost entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            Update(entity, new List<DalTag>());
        }

        public void Update(DalPost entity, IEnumerable<DalTag> tags)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (tags == null)
                throw new ArgumentNullException(nameof(entity));

            var post = context.Set<Post>().FirstOrDefault(p => p.PostId == entity.Id);

            if (post != null)
            {
                post.Tags.Clear();

                foreach (var dalTag in tags)
                {
                    var tag = context.Set<Tag>().FirstOrDefault(t => t.Name == dalTag.Name);
                    if (tag != null)
                        post.Tags.Add(tag);
                }

                post.Title = entity.Title;
                post.Description = entity.Description;
            }
        }

        public void Delete(DalPost entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var post = context.Set<Post>().SingleOrDefault(p => p.PostId == entity.Id);

            if (post != null)
                context.Set<Post>().Remove(post);
        }
        #endregion

        #region Get operations
        public DalPost GetById(int key) => context.Set<Post>().FirstOrDefault(p => p.PostId == key)?.ToDalPost();
        public IEnumerable<DalPost> GetAll() => context.Set<Post>().ToList().Select(p => p.ToDalPost()).OrderByDescending(p => p.Id);
        /// <summary>
        /// This method gets DAL posts for one page by using give parameters such as size of page and number of page.
        /// </summary>
        /// <param name="pageSize">The number of items on one page.</param>
        /// <param name="pageNumber">Number of page.</param>
        /// <returns>Returns collection of DAL posts for one page.</returns>
        public IEnumerable<DalPost> GetPostsForPage(int pageSize, int pageNumber)
        {
            return context.Set<Post>().OrderByDescending(p => p.PostId).Skip(pageNumber * pageSize).Take(pageSize).ToList().Select(p => p.ToDalPost());
        }
        /// <summary>
        /// This method returns all DAL posts which were posted by this user.
        /// </summary>
        /// <param name="userId">User's id.</param>
        /// <returns>Returns collection of DAL posts.</returns>
        public IEnumerable<DalPost> GetDalPostsByUserId(int userId)
        {
            return context.Set<User>().FirstOrDefault(u => u.UserId == userId)?.Posts.ToList().Select(p => p.ToDalPost());
        }
        /// <summary>
        /// This method returns all DAL posts which contain the give hashtag.
        /// </summary>
        /// <param name="tagName">Name of hashtag.</param>
        /// <returns>Returns collection of DAL posts.</returns>
        public IEnumerable<DalPost> GetDalPostsByTagName(string tagName) => context.Set<Tag>().FirstOrDefault(t => t.Name == tagName)?.Posts.Select(p => p.ToDalPost());
        #endregion

        /// <summary>
        /// This method adds tags to a post.
        /// </summary>
        /// <param name="postId">Id of post.</param>
        /// <param name="tags">Array of names of tags.</param>
        public void AddTagsToPost(int postId, string[] tags)
        {
            var post = context.Set<Post>().FirstOrDefault(p => p.PostId == postId);

            if (post != null)
            {
                if (tags != null)
                {
                    foreach (var tagName in tags)
                    {
                        var tag = context.Set<Tag>().FirstOrDefault(t => t.Name == tagName);
                        if (tag != null)
                            post.Tags.Add(tag);
                    }
                }
            }
        }

        /// <summary>
        /// This method returns number of posts in the database.
        /// </summary>
        /// <returns>Returns number of posts in the database.</returns>
        public int Count() => context.Set<Post>().Count();

        private readonly DbContext context;
    }
}

