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
            var comment = context.Set<Comment>().FirstOrDefault(c => c.CommentId == entity.Id);

            if (comment == default(Comment))
            {
                comment = entity.ToOrmComment();
                context.Set<Comment>().Add(comment);
            }

            comment.Text = entity.Text;
            comment.PublishDate = entity.PublishDate;
            //comment.PostId = entity.PostId;
            //comment.UserId = entity.UserId;
            context.Entry(comment).State = EntityState.Modified;
        }

        public void Delete(DalComment entity)
        {
            if (entity == null)
                return;

            var comment = context.Set<Comment>().Single(u => u.CommentId == entity.Id);

            if (comment != default(Comment))
                context.Set<Comment>().Remove(comment);
        }
        #endregion

        #region Get operations
        // NULLABLE
        public DalComment GetById(int? key) => context.Set<Comment>().FirstOrDefault(u => u.CommentId == key)?.ToDalComment();

        public IEnumerable<DalComment> GetAll() => context.Set<Comment>().ToList().Select(c => c.ToDalComment());

        public DalComment GetOneByPredicate(Expression<Func<DalComment, bool>> predicate) => GetAllByPredicate(predicate).FirstOrDefault();

        public IEnumerable<DalComment> GetAllByPredicate(Expression<Func<DalComment, bool>> predicate)
        {
            var visitor = new PredicateExpressionVisitor<DalComment, Comment>(Expression.Parameter(typeof(Comment), predicate.Parameters[0].Name));
            var express = Expression.Lambda<Func<Comment, bool>>(visitor.Visit(predicate.Body), visitor.NewParameterExp);
            var final = context.Set<Comment>().Where(express).ToList();
            return final.Select(comment => comment.ToDalComment());
        }

        // Fix it
        //public IEnumerable<DalComment> GetDalCommentsByUserId(int userId) => context.Set<Comment>().Where(c => c.UserId == userId).ToList().Select(c => c.ToDalComment());
        public IEnumerable<DalComment> GetDalCommentsByUserId(int userId) => context.Set<User>().FirstOrDefault(u => u.UserId == userId)?.Comments.ToList().Select(c => c.ToDalComment());

        //Fix it
        //public IEnumerable<DalComment> GetDalCommentsByPostId(int postId) => context.Set<Comment>().Where(c => c.PostId == postId).ToList().Select(c => c.ToDalComment());
        public IEnumerable<DalComment> GetDalCommentsByPostId(int postId) => context.Set<Post>().FirstOrDefault(p => p.PostId == postId)?.Comments.ToList().Select(c => c.ToDalComment()); 
        #endregion

        private readonly DbContext context;
    }
}
