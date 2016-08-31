﻿using System;
using System.Linq;
using System.Web.Mvc;
using BLL.Interfacies.Entities;
using BLL.Interfacies.Services;
using MvcPL.Infrastructure.Mappers;
using MvcPL.Models.Post;
using System.Diagnostics;
using MvcPL.Models.User;
using System.Collections.Generic;

namespace MvcPL.Controllers
{
    public class PostController : Controller
    {
        public PostController(IPostService postService, ITagService tagService, IUserService userService)
        {
            this.postService = postService;
            this.tagService = tagService;
            this.userService = userService;
        }

        public ActionResult Index()
        {
            var mvcPosts = new List<PostViewModel>();

            foreach(var bllPost in postService.GetAll())
            {
                var mvcPost = bllPost.ToMvcPost();

                mvcPost.Author = userService.GetById(bllPost.UserId).ToMvcUser();

                Debug.Write("\nTest collection of tags. PostId = " + bllPost.Id + " .Number of tags: " + tagService.GetTagsOfPost(bllPost.Id).Count());
                

                foreach (var tag in tagService.GetTagsOfPost(bllPost.Id).Select(tag => tag.ToMvcTag()))
                    mvcPost.Tags.Add(tag);
                mvcPost.NumberOfLikes = postService.GetLikesByPostId(bllPost.Id).Count();
                mvcPost.NumberOfComments = postService.GetCommentsByPostId(bllPost.Id).Count();

                mvcPosts.Add(mvcPost);
            }

            return View(mvcPosts);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new CreatePostViewModel();
            model.TagList = new SelectList(tagService.GetAll().Select(tag => tag.ToMvcTag().Name));
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreatePostViewModel createPostViewModel, string[] tags)
        {   
            // Should take from current user. 
            createPostViewModel.UserId = 1;

            postService.Create(createPostViewModel.ToBllPost(), tags);
            return RedirectToAction("Index");
        }

        //[Authorize(Roles = "Admin")]
        public ActionResult Delete(int postId)
        {
            var post = postService.GetById(postId);
            postService.Delete(post);
            return RedirectToAction("Index");
        }

        // Like and dislike

        // Add comment

        // Remove comment

        // Find by tag

        private readonly IPostService postService;
        private readonly ITagService tagService;
        private readonly IUserService userService;
    }
}