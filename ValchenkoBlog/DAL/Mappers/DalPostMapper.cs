﻿using DAL.Interfacies.DTO;
using ORM.Models;
using System.Collections.Generic;

namespace DAL.Mappers
{
    public static class DalPostMapper
    {
        public static DalPost ToDalPost(this Post ormPost)
        {
            if (ormPost == null)
                return null;

            return new DalPost
            {
                Id = ormPost.PostId,
                Title = ormPost.Title,
                Description = ormPost.Description,
                PublishDate = ormPost.PublishDate,
                UserId = ormPost.User.UserId
            };
        }

        public static Post ToOrmPost(this DalPost dalPost)
        {
            if (dalPost == null)
                return null;

            return new Post
            {
                Title = dalPost.Title,
                Description = dalPost.Description,
                PublishDate = dalPost.PublishDate
                // Add new
                /*User = new User(),
                Tags = new HashSet<Tag>(),
                Comments = new HashSet<Comment>(),
                Likes = new HashSet<Like>()*/
            };
        }
    }
}
