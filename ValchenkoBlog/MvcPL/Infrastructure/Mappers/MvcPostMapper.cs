using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Interfacies.Entities;
using MvcPL.Models.Post;
using MvcPL.Models.User;
using MvcPL.Models.Tag;
using MvcPL.Models.Comment;

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
                PublishDate = bllPost.PublishDate.ToShortDateString(),
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
        public static PostEntity ToBllPost(this EditPostViewModel editPostViewModel, IEnumerable<string> tags)
        {
            if (editPostViewModel == null)
                throw new ArgumentNullException(nameof(editPostViewModel));

            return new PostEntity
            {
                Id = editPostViewModel.Id,
                Title = editPostViewModel.Title,
                Description = editPostViewModel.Description,
                Tags = tags?.Select(tag => new TagEntity() { Name = tag }).ToList() ?? new List<TagEntity>()
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
            if (bllPost == null)
                return null;

            return new DetailsPostViewModel
            {
                Id = bllPost.Id,
                Title = bllPost.Title,
                Description = bllPost.Description,
                PublishDate = bllPost.PublishDate.ToShortDateString(),
                Author = bllPost.User?.ToMvcUser() ?? new UserViewModel(),
                NumberOfLikes = bllPost.Likes.Count,
                Tags = bllPost.Tags?.Select(tag => tag.ToMvcTag()).ToList() ?? new List<TagViewModel>(),
                Comments = bllPost.Comments?.Select(comment => comment.ToMvcComment()).ToList() ?? new List<CommentViewModel>()
            };
        }
    }
}