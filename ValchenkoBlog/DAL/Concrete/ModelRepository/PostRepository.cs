﻿using System;
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


        #region CRUD operations
        public int Create(DalPost entity/*, List<DalTag> tags*/)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var post = entity.ToOrmPost();
            // Test. I add post to the user before it.
            int createdPostId = context.Set<Post>().Add(post).PostId;

            /*foreach (var dalTag in tags)
                context.Set<Tag>().FirstOrDefault(t => t.Name == dalTag.Name)?.Posts.Add(post);*/

            context.Set<User>().FirstOrDefault(user => user.UserId == entity.UserId)?.Posts.Add(post);

            // Add new
            return createdPostId;
        }

        public int Update(DalPost entity)
        {
            var post = context.Set<Post>().FirstOrDefault(p => p.PostId == entity.Id);

            if (post == default(Post))
            {
                post = entity.ToOrmPost();
                return context.Set<Post>().Add(post).PostId;
                //return;
            }

            post.Title = entity.Title;
            post.Description = entity.Description;
            post.PublishDate = entity.PublishDate;
            //post.UserId = entity.UserId;
            context.Entry(post).State = EntityState.Modified;

            // Add new 
            return entity.Id;
        }

        public void Delete(DalPost entity)
        {
            if (entity == null)
                return;

            var post = context.Set<Post>().SingleOrDefault(p => p.PostId == entity.Id);

            if (post != default(Post))
                context.Set<Post>().Remove(post);
        } 
        #endregion


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
        public IEnumerable<DalPost> GetDalPostsByUserId(int userId) => context.Set<User>().FirstOrDefault(u => u.UserId == userId)?.Posts.ToList().Select(p => p.ToDalPost());


        // Is it normal?
        public IEnumerable<DalPost> GetDalPostsByTagName(string tagName) => context.Set<Tag>().FirstOrDefault(t => t.Name == tagName)?.Posts.Select(p => p.ToDalPost());

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

        private readonly DbContext context;
    }
}

