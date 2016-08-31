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
                //Id = mvcPost.Id,
                Title = mvcPost.Title,
                Description = mvcPost.Description,
                PublishDate = DateTime.Now,
                UserId = mvcPost.UserId
            };
        }

        public static PostViewModel ToMvcPost(this PostEntity bllPost)
        {
            return new PostViewModel
            {
                Id = bllPost.Id,
                Title = bllPost.Title,
                Description = bllPost.Description,
                PublishDate = bllPost.PublishDate.ToShortTimeString(),
                // etc. in controller
            };
        }
    }
}