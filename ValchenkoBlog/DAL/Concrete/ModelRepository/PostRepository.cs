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
    public class PostRepository : IPostRepository
    {
        public PostRepository(DbContext context)
        {
            this.context = context;
        }

        /*public void AddLike(DalLike like)
        {
            if (like == null)
                return;

            // Where is 'if default' part?

            var post = context.Set<Post>().First(p => p.PostId == like.PostId);
            context.Set<Post>().Attach(post);
            post.Likes.Add(like.ToOrmLike());
            //unitOfWork.Commit();
        }

        public void RemoveLike(DalLike like)
        {
            var post = context.Set<Post>().First(p => p.PostId == like.PostId);
            post.Likes.Remove(like.ToOrmLike());
            //// Two conditions were here.
            var ormLike = context.Set<Like>().First(l => l.Post.PostId == like.PostId);
            context.Set<Like>().Remove(ormLike);
            //unitOfWork.Commit();
        }*/

        public void Create(DalPost entity)
        {
            if (entity == null)
                return;

            var post = entity.ToOrmPost();
            context.Set<Post>().Add(post);
            //unitOfWork.Commit();
        }

        public void Delete(DalPost entity)
        {
            if (entity == null)
                return;

            var post = context.Set<Post>().SingleOrDefault(p => p.PostId == entity.Id);

            if(post != default(Post))
                context.Set<Post>().Remove(post);

            //unitOfWork.Commit();
        }

        public void Update(DalPost entity)
        {
            var post = context.Set<Post>().FirstOrDefault(p => p.PostId == entity.Id);

            if (post == default(Post))
            {
                post = entity.ToOrmPost();
                context.Set<Post>().Add(post);
                return;
            }

            post.Title = entity.Title;
            post.Description = entity.Description;
            post.PublishDate = entity.PublishDate;
            //post.UserId = entity.UserId;
            context.Entry(post).State = EntityState.Modified;
        }

        public DalPost GetById(int key) => context.Set<Post>().FirstOrDefault(p => p.PostId == key)?.ToDalPost();

        public IEnumerable<DalPost> GetAll() => context.Set<Post>().ToList().Select(p => p.ToDalPost());

        public DalPost GetOneByPredicate(Expression<Func<DalPost, bool>> predicate) => GetAllByPredicate(predicate).FirstOrDefault();

        public IEnumerable<DalPost> GetAllByPredicate(Expression<Func<DalPost, bool>> predicate)
        {
            var visitor = new PredicateExpressionVisitor<DalPost, Post>(Expression.Parameter(typeof(Post), predicate.Parameters[0].Name));
            var express = Expression.Lambda<Func<Post, bool>>(visitor.Visit(predicate.Body), visitor.NewParameterExp);
            var final = context.Set<Post>().Where(express).ToList();
            return final.Select(post => post.ToDalPost());
        }

        // Fix it
        //public IEnumerable<DalPost> GetDalPostsByUserId(int userId) => context.Set<Post>().Where(p => p.UserId == userId).ToList().Select(p => p.ToDalPost());
        public IEnumerable<DalPost> GetDalPostsByUserId(int userId) => context.Set<User>().FirstOrDefault(u => u.UserId == userId)?.Posts.ToList().Select(p => p.ToDalPost());


        // Is it normal?
        public IEnumerable<DalPost> GetDalPostsByTagName(string tagName) => context.Set<Tag>().FirstOrDefault(t => t.Name == tagName)?.Posts.Select(p => p.ToDalPost());

        //private UnitOfWork unitOfWork;
        private DbContext context;
    }
}

