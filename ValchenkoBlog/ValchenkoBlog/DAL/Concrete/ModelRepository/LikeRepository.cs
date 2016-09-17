using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DAL.Interfacies.DTO;
using DAL.Interfacies.Repository.ModelRepository;
using ORM.Models;
using DAL.Mappers;
using System.Data.Entity;

namespace DAL.Concrete.ModelRepository
{
    public class LikeRepository : ILikeRepository
    {
        public LikeRepository(DbContext context)
        {
            this.context = context;
        }

        #region CRUD operations
        public void Create(DalLike entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var like = entity.ToOrmLike();
            like.User = context.Set<User>().FirstOrDefault(u => u.UserId == entity.UserId);
            like.Post = context.Set<Post>().FirstOrDefault(p => p.PostId == entity.PostId);

            context.Set<Post>().FirstOrDefault(p => p.PostId == entity.PostId)?.Likes.Add(like);
        }

        public void Delete(DalLike entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var like = context.Set<Like>().Single(l => l.LikeId == entity.Id);

            if (like != null)
                context.Set<Like>().Remove(like);
        }

        public void Update(DalLike entity)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Get operations
        public DalLike GetById(int key) => context.Set<Like>().FirstOrDefault(l => l.LikeId == key)?.ToDalLike();
        public IEnumerable<DalLike> GetAll() => context.Set<Like>().ToList().Select(l => l.ToDalLike());
        public IEnumerable<DalLike> GetDalLikesByUserId(int userId) => context.Set<User>().FirstOrDefault(u => u.UserId == userId)?.Likes.Select(like => like.ToDalLike());
        public IEnumerable<DalLike> GetDalLikesByPostId(int postId) => context.Set<Post>().FirstOrDefault(p => p.PostId == postId)?.Likes.Select(like => like.ToDalLike());
        public DalLike GetDalLikeByPostIdAndUserId(int userId, int postId)
        {
            return context.Set<Like>().FirstOrDefault(l => l.User.UserId == userId && l.Post.PostId == postId)?.ToDalLike();
        }
        #endregion

        public void DeleteLikesFromPost(int postId)
        {
            var likes = context.Set<Like>().Where(like => like.Post.PostId == postId);

            if (likes != null)
                foreach (var like in likes)
                    context.Set<Like>().Remove(like);
        }

        private readonly DbContext context;
    }
}
