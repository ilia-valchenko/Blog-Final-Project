using System;
using System.Collections.Generic;
using BLL.Interfacies.Entities;
using MvcPL.Models;
using MvcPL.Models.Post;
using MvcPL.Models.User;

namespace MvcPL.Infrastructure.Mappers
{
    public static class MvcPostMapper
    {
        public static PostEntity ToBllPost(this CreatePostViewModel mvcPost)
        {
            // if null

            return new PostEntity
            {
                Title = mvcPost.Title,
                Description = mvcPost.Description,
                PublishDate = DateTime.Now,
                // Cookie
                User = new UserEntity
                {
                    Id = mvcPost.UserId
                }
            };
        }

        public static PostViewModel ToMvcPost(this PostEntity bllPost)
        {
            // is null

            var post = new PostViewModel
            {
                Id = bllPost.Id,
                Title = bllPost.Title,
                Description = bllPost.Description,
                PublishDate = bllPost.PublishDate.ToShortTimeString(),
                Author = bllPost.User.ToMvcUser(),
                NumberOfComments = bllPost.Comments.Count,
                NumberOfLikes = bllPost.Likes.Count
            };

            foreach (var bllTag in bllPost.Tags)
                post.Tags.Add(bllTag.ToMvcTag());

            return post;
        }

        public static DetailsPostViewModel ToMvcDetailsPost(this PostEntity bllPost)
        {
            // if null

            var post = new DetailsPostViewModel
            {
                Id = bllPost.Id,
                Title = bllPost.Title,
                Description = bllPost.Description,
                PublishDate = bllPost.PublishDate.ToShortDateString(),
                Author = new UserViewModel
                {
                    Id = bllPost.User.Id,
                    Nickname = bllPost.User.Nickname,
                    Avatar = bllPost.User.Avatar
                },
                NumberOfLikes = bllPost.Likes.Count
            };

            foreach (var bllTag in bllPost.Tags)
                post.Tags.Add(bllTag.ToMvcTag());

            foreach (var bllComment in bllPost.Comments)
                post.Comments.Add(bllComment.ToMvcComment());
        }
    }
}