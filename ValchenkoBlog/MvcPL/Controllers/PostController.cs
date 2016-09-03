using System;
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

        public ActionResult Index() => View(postService.GetAll().Select(post => post.ToMvcPost()));

        [HttpGet]
        public ActionResult Create()
        {
            var model = new CreatePostViewModel();
            model.TagList = new SelectList(tagService.GetAll().Select(tag => tag.ToMvcTag().Name));
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreatePostViewModel createPostViewModel, string[] namesOfTags)
        {   
            // Now TagList and SelectedList is null. I should bind it from form.

            // Should take from current user. 
            createPostViewModel.UserId = 9;

            // Should PostService takes post and tags as an arguments?
            int idOfCreatedPost = postService.Create(createPostViewModel.ToBllPost());
            postService.AddTagsToPost(idOfCreatedPost, namesOfTags);

            return RedirectToAction("Index");
        }

        //[Authorize(Roles = "Admin")]
        public ActionResult Delete(int postId)
        {
            var post = postService.GetById(postId);
            postService.Delete(post);
            return RedirectToAction("Index");
        }

        public ActionResult Like(/*int postId*/)
        {
            // We also should know the id of user.
            // int postId, int userId
            var like = new LikeEntity
            {
                PostId = 13,
                UserId = 9
            };

            postService.AddLike(like);
            return RedirectToAction("Index");
        }

        // Add comment

        // Remove comment

        // Find by tag

        private readonly IPostService postService;
        private readonly ITagService tagService;
        private readonly IUserService userService;
    }
}