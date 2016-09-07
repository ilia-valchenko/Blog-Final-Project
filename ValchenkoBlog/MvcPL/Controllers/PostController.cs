﻿using System;
using System.Linq;
using System.Web.Mvc;
using BLL.Interfacies.Entities;
using BLL.Interfacies.Services;
using MvcPL.Infrastructure.Mappers;
using MvcPL.Models.Post;
using System.Net;
using System.Collections.Generic;
using MvcPL.Models.Tag;

namespace MvcPL.Controllers
{
    public class PostController : Controller
    {
        public PostController(IPostService postService, 
                              ITagService tagService, 
                              IUserService userService,
                              ICommentService commentService)
        {
            this.postService = postService;
            this.tagService = tagService;
            this.userService = userService;
            this.commentService = commentService;
        }

        public ActionResult Index() => View(postService.GetAll().Select(post => post.ToMvcPost()));

        #region Create
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
            createPostViewModel.UserId = 11;

            // Should PostService takes post and tags as an arguments?
            //int idOfCreatedPost = postService.Create(createPostViewModel.ToBllPost());
            var bllPost = createPostViewModel.ToBllPost();
            // add tags
            foreach (var tagName in namesOfTags)
                bllPost.Tags.Add(new TagEntity { Name = tagName });

            postService.Create(bllPost);

            //postService.AddTagsToPost(idOfCreatedPost, namesOfTags);

            return RedirectToAction("Index");
        } 
        #endregion

        #region Delete
        // НЕСЕТ ПОТЕНЦИАЛЬНУЮ УЯЗВИМОСТЬ!
        // GET: Posts/Delete/5
        //[Authorize(Roles = "Admin")]
        //[HttpGet]
        public ActionResult Delete(int? id)
        {
            // NULLABLE
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var post = postService.GetById(id)?.ToMvcPost();

            if (post == null)
                return HttpNotFound();

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            postService.Delete(postService.GetById(id));
            return RedirectToAction("Index", "Post");
        }
        #endregion

        #region Edit
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            EditPostViewModel model = postService.GetById(id)?.ToMvcEditPost();

            if (model == null)
                return HttpNotFound();

            model.TagList = new SelectList(tagService.GetAll().Select(tag => tag.ToMvcTag().Name));

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditPostViewModel model, string[] namesOfTags)
        {
            var post = model.ToBllPost();
                
            if(namesOfTags != null)
                foreach (var tagName in namesOfTags)
                    post.Tags.Add(new TagEntity { Name = tagName });

            postService.Update(post);
            return RedirectToAction("Index");
        } 
        #endregion

        //[HttpGet]
        public ActionResult Details(int id)
        {
            // NULLABLE
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var post = postService.GetById(id)?.ToMvcDetailsPost();
            
            if (post == null)
                return HttpNotFound(); // Redirect to custom error page

            var comments = commentService.GetCommentsByPostId(id);
            if (comments != null)
                foreach (var bllComment in comments)
                    post.Comments.Add(bllComment.ToMvcComment());

            return View(post);
        }

        [HttpPost]
        public ActionResult Like(int id)
        {
            // We also should know the id of user.
            // int postId, int userId
            var like = new LikeEntity
            {
                PostId = id,
                UserId = 11
            };

            postService.Like(like);

            return Json(postService.GetById(id).Likes.Count);
        }

        public ActionResult AddComment(int postId, string text) 
        {
            var comment = new CommentEntity
            {
                Text = "Maharadga",
                PublishDate = DateTime.Now,
                Post = new PostEntity
                {
                    Id = postId
                },
                User = new UserEntity
                {
                    Id = 11 
                }
            };

            postService.AddComment(comment);

            return Json(commentService.GetCommentsByPostId(postId).Select(c => c.ToMvcComment()));

            //return Json("It works!");
        }

        // Remove comment

        public ActionResult SearchByTag(string tagname)
        {
            ViewBag.TagName = tagname;
            return View(postService.GetPostsByTagName(tagname).Select(post => post.ToMvcPost()));
        }

        private readonly IPostService postService;
        private readonly ITagService tagService;
        private readonly IUserService userService;
        private readonly ICommentService commentService;
    }
}