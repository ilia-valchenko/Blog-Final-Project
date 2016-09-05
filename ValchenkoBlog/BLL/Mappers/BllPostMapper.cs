using System.Collections.Generic;
using BLL.Interfacies.Entities;
using DAL.Interfacies.DTO;

namespace BLL.Mappers
{
    public static class BllPostMapper
    {
        public static DalPost ToDalPost(this PostEntity bllPost)
        {
            if (bllPost == null)
                return null;

            return new DalPost
            {
                Id = bllPost.Id,
                Title = bllPost.Title,
                Description = bllPost.Description,
                PublishDate = bllPost.PublishDate,
                UserId = bllPost.User.Id
            };
        }

        public static PostEntity ToBllPost(this DalPost dalPost)
        {
            if (dalPost == null)
                return null;

            return new PostEntity
            {
                Id = dalPost.Id,
                Title = dalPost.Title,
                Description = dalPost.Description,
                PublishDate = dalPost.PublishDate,
                Tags = new List<TagEntity>(),
                Comments = new List<CommentEntity>(),
                Likes = new List<LikeEntity>(),
                User = new UserEntity()
            };
        }
    }
}
