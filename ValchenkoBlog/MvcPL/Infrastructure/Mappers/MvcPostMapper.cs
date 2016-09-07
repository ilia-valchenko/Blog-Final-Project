using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Interfacies.Entities;
using MvcPL.Models.Post;
using MvcPL.Models.User;
using MvcPL.Models.Tag;

namespace MvcPL.Infrastructure.Mappers
{
    public static class MvcPostMapper
    {
        public static PostViewModel ToMvcPost(this PostEntity bllPost)
        {
            if (bllPost == null)
                return null;

            return new PostViewModel
            {
                Id = bllPost.Id,
                Title = bllPost.Title,
                Description = bllPost.Description,
                PublishDate = bllPost.PublishDate.ToShortTimeString(),
                Author = bllPost.User.ToMvcUser() ?? new UserViewModel(),
                NumberOfComments = bllPost.Comments?.Count ?? 0,
                NumberOfLikes = bllPost.Likes?.Count ?? 0,
                Tags = bllPost.Tags?.Select(tag => new TagViewModel { Name = tag.Name }).ToList() ?? new List<TagViewModel>()
            };
        }

        public static EditPostViewModel ToMvcEditPost(this PostEntity bllPost)
        {
            if (bllPost == null)
                return null;

            return new EditPostViewModel
            {
                Id = bllPost.Id,
                Title = bllPost.Title,
                Description = bllPost.Description
            };
        }
        public static PostEntity ToBllPost(this EditPostViewModel editPostViewModel)
        {
            if (editPostViewModel == null)
                throw new ArgumentNullException(nameof(editPostViewModel));

            return new PostEntity
            {
                Id = editPostViewModel.Id,
                Title = editPostViewModel.Title,
                Description = editPostViewModel.Description,
                // Tag list
                // Selected list
            };
        }

        public static PostEntity ToBllPost(this CreatePostViewModel mvcPost, IEnumerable<string> tags)
        {
            if (mvcPost == null)
                throw new ArgumentNullException(nameof(mvcPost));

            return new PostEntity
            {
                Title = mvcPost.Title,
                Description = mvcPost.Description,
                PublishDate = DateTime.Now,
                User = new UserEntity { Id = mvcPost.UserId },
                Tags = tags?.Select(tag => new TagEntity() { Name = tag }).ToList() ?? new List<TagEntity>()
            };

            
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

            return post;
        }
    }
}