﻿using System;
using BLL.Interfacies.Entities;
using MvcPL.Models.User;
using MvcPL.Models.Comment;

namespace MvcPL.Infrastructure.Mappers
{
    public static class MvcCommentMapper
    {
        public static CommentEntity ToBllComment(this CommentViewModel mvcComment)
        {
            if (mvcComment == null)
                throw new ArgumentNullException(nameof(mvcComment));

            return new CommentEntity()
            {
                Text = mvcComment.Text,
                PublishDate = DateTime.Now,
                Post = new PostEntity { Id = mvcComment.Post.Id },
                User = new UserEntity { Id = mvcComment.User.Id }
            };
        }

        public static CommentViewModel ToMvcComment(this CommentEntity bllComment)
        {
            if (bllComment == null)
                return null;

            var date = bllComment.PublishDate;

            return new CommentViewModel
            {
                Id = bllComment.Id,
                Text = bllComment.Text,
                PublishDate = date.ToShortDateString() + $" {date.Hour}:{date.Second}",
                User = bllComment.User?.ToMvcUser() ?? new UserProfileViewModel()
            };
        }
    }
}