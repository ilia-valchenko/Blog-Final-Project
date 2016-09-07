using System;
using BLL.Interfacies.Entities;
using MvcPL.Models.Post;
using MvcPL.Models.User;
using MvcPL.Models.Comment;

namespace MvcPL.Infrastructure.Mappers
{
    public static class MvcCommentMapper
    {
        public static CommentEntity ToBllComment(this CommentViewModel mvcComment)
        {
            // if null
            return new CommentEntity()
            {
                // without id
                Text = mvcComment.Text,
                PublishDate = DateTime.Now,
                Post = new PostEntity
                {
                    Id = mvcComment.Post.Id
                },
                User = new UserEntity
                {
                    Id = mvcComment.Id
                }
            };
        }

        public static CommentViewModel ToMvcComment(this CommentEntity bllComment)
        {
            // if null
            return new CommentViewModel
            {
                Id = bllComment.Id,
                Text = bllComment.Text,
                PublishDate = bllComment.PublishDate.ToString(),
                // Post is unnecessary
                User = new UserViewModel
                {
                    Id = bllComment.User.Id,
                    Nickname = bllComment.User.Nickname,
                    Avatar = bllComment.User.Avatar
                }
            };
        }
    }
}