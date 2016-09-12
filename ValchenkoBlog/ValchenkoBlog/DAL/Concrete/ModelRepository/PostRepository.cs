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

            var post = entity.ToOrmPost();

            if (post != null)
            {
                if (tags != null)
                {
                    foreach (var dalTag in tags)
                    {
                        var tag = context.Set<Tag>().FirstOrDefault(t => t.Name == dalTag.Name);
                        if (tag != null)
                            post.Tags.Add(tag);
                    }
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
            var post = context.Set<Post>().FirstOrDefault(p => p.PostId == entity.Id);

            if (post != null)
            {
                post.Tags.Clear();

                if (tags != null)
                {
                    foreach (var dalTag in tags)
                    {
                        var tag = context.Set<Tag>().FirstOrDefault(t => t.Name == dalTag.Name);
                        if (tag != null)
                            post.Tags.Add(tag);
                    }
                }
            }

            post.Title = entity.Title;
            post.Description = entity.Description;
        }

        public void Delete(DalPost entity)
        {
            if (entity == null)
                return;

            var post = context.Set<Post>().SingleOrDefault(p => p.PostId == entity.Id);

            // Test
            // But they still exist
            //post.Likes.Clear();

            if (post != default(Post))
                context.Set<Post>().Remove(post);

            // And what happend with list of tags, likes and comments?
        }
        #endregion

        #region Get operations
        public DalPost GetById(int key) => context.Set<Post>().FirstOrDefault(p => p.PostId == key)?.ToDalPost();

        public IEnumerable<DalPost> GetAll() => context.Set<Post>().ToList().Select(p => p.ToDalPost()).OrderByDescending(p => p.Id);

        public IEnumerable<DalPost> GetPostsForPage(int pageSize, int pageNumber)
        {
            return context.Set<Post>().OrderByDescending(p => p.PostId).Skip(pageNumber * pageSize).Take(pageSize).ToList().Select(p => p.ToDalPost());
        }

        public DalPost GetOneByPredicate(Expression<Func<DalPost, bool>> predicate) => GetAllByPredicate(predicate).FirstOrDefault();

        public IEnumerable<DalPost> GetAllByPredicate(Expression<Func<DalPost, bool>> predicate)
        {
            var visitor = new PredicateExpressionVisitor<DalPost, Post>(Expression.Parameter(typeof(Post), predicate.Parameters[0].Name));
            var express = Expression.Lambda<Func<Post, bool>>(visitor.Visit(predicate.Body), visitor.NewParameterExp);
            var final = context.Set<Post>().Where(express).ToList();
            return final.Select(post => post.ToDalPost());
        }

        // Fix it
        public IEnumerable<DalPost> GetDalPostsByUserId(int userId) {
            return context.Set<User>().FirstOrDefault(u => u.UserId == userId)?.Posts.ToList().Select(p => p.ToDalPost());
        } 

        // Is it normal?
        public IEnumerable<DalPost> GetDalPostsByTagName(string tagName) => context.Set<Tag>().FirstOrDefault(t => t.Name == tagName)?.Posts.Select(p => p.ToDalPost()); 
        #endregion

        public void AddTagsToPost(int postId, string[] tags)
        {
            var post = context.Set<Post>().FirstOrDefault(p => p.PostId == postId);

            if (post != null)
            {
                if(tags != null)
                {
                    foreach(var tagName in tags)
                    {
                        var tag = context.Set<Tag>().FirstOrDefault(t => t.Name == tagName);
                        if (tag != null)
                            post.Tags.Add(tag);
                    }
                }
            }
        }

        public int Count() => context.Set<Post>().Count();

        private readonly DbContext context;
    }
}

