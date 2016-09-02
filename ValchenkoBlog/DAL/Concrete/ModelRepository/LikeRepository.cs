using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DAL.Interfacies.DTO;
using DAL.Interfacies.Repository.ModelRepository;
using ORM.Models;
using DAL.Mappers;
using DAL.Interfacies.Helper;
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
        public int Create(DalLike entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var like = entity.ToOrmLike();
            // Add new
            int createdLikeId = context.Set<Like>().Add(like).LikeId;

            var post = context.Set<Post>().FirstOrDefault(p => p.PostId == entity.PostId);
            var user = context.Set<User>().FirstOrDefault(u => u.UserId == entity.UserId);


            return createdLikeId;
            //context.Set<Post>().FirstOrDefault(p => p.PostId == entity.PostId)?.Likes.Add(like);
            //context.Set<Like>().Add(like);
        }

        public int Update(DalLike entity)
        {
            var like = context.Set<Like>().FirstOrDefault(l => l.LikeId == entity.Id);

            if (like == default(Like))
            {
                like = entity.ToOrmLike();
                return context.Set<Like>().Add(like).LikeId;
                //return;
            }

            //like.PostId = entity.PostId;
            //like.UserId = entity.UserId;
            context.Entry(like).State = EntityState.Modified;

            // Add new
            return entity.Id;
        }

        public void Delete(DalLike entity)
        {
            if (entity == null)
                return;

            var like = context.Set<Like>().Single(l => l.LikeId == entity.Id);

            if (like != default(Like))
                context.Set<Like>().Remove(like);
        } 
        #endregion

        public DalLike GetById(int key) => context.Set<Like>().FirstOrDefault(l => l.LikeId == key)?.ToDalLike();

        public IEnumerable<DalLike> GetAll() => context.Set<Like>().ToList().Select(l => l.ToDalLike());

        public DalLike GetOneByPredicate(Expression<Func<DalLike, bool>> predicate) => GetAllByPredicate(predicate).FirstOrDefault();

        public IEnumerable<DalLike> GetAllByPredicate(Expression<Func<DalLike, bool>> predicate)
        {
            var visitor = new PredicateExpressionVisitor<DalLike, Like>(Expression.Parameter(typeof(Like), predicate.Parameters[0].Name));
            var express = Expression.Lambda<Func<Like, bool>>(visitor.Visit(predicate.Body), visitor.NewParameterExp);
            var final = context.Set<Like>().Where(express).ToList();
            return final.Select(like => like.ToDalLike());
        }

        //Fix it
        //public IEnumerable<DalLike> GetDalLikesByUserId(int userId) => context.Set<Like>().Where(l => l.UserId == userId).ToList().Select(l => l.ToDalLike());
        public IEnumerable<DalLike> GetDalLikesByUserId(int userId) => context.Set<User>().FirstOrDefault(u => u.UserId == userId)?.Likes.Select(like => like.ToDalLike());

        public IEnumerable<DalLike> GetDalLikesByPostId(int postId) => context.Set<Post>().FirstOrDefault(p => p.PostId == postId)?.Likes.Select(like => like.ToDalLike());

        private readonly DbContext context;
    }
}
