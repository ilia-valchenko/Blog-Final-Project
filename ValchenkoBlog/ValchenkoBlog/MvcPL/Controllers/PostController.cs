using System;
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

        public ActionResult Index(int page = 0)
        {
            const int pageSize = 6;
            int count = postService.Count();

            ViewBag.NumberOfPages = (count / pageSize) - (count % pageSize == 0 ? 1 : 0);
            ViewBag.CurrentPage = page;

            return View(postService.GetPostsForPage(pageSize, page).Select(post => post.ToMvcPost()));
        }

        #region Create
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            var model = new CreatePostViewModel();
            model.TagList = new SelectList(tagService.GetAll().Select(tag => tag.ToMvcTag().Name));
            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreatePostViewModel createPostViewModel, string[] namesOfTags)
        {
            // Авторизация через HttpModule AutorixzationCkooki ложить еще и id, чтобы не доставать из базы
            // или логине в куки 
            createPostViewModel.UserId = userService.GetUserEntityByNickname(User.Identity.Name).Id;

            postService.Create(createPostViewModel.ToBllPost(namesOfTags));

            //postService.AddTagsToPost(idOfCreatedPost, namesOfTags);

            return RedirectToAction("Index");
        } 
        #endregion

        #region Delete
        // НЕСЕТ ПОТЕНЦИАЛЬНУЮ УЯЗВИМОСТЬ!
        // GET: Posts/Delete/5
        //[Authorize(Roles = "admin")]
        //[HttpGet]
        public ActionResult Delete(int? id)
        {
            // NULLABLE
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var post = postService.GetById((int)id)?.ToMvcPost();

            if (post == null)
            {
                Response.StatusCode = 404;
                return RedirectToAction("NotFound", "Error");
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        //[Authorize(Roles = "admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            postService.Delete(postService.GetById(id));
            return RedirectToAction("Index", "Post");
        }
        #endregion

        #region Edit
        //[Authorize(Roles = "admin")]
        // Редактировать пост может только его сощдатель.
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var post = postService.GetById(id);

            if (post == null)
                return HttpNotFound(); // Redirect to custom error page

            if (User.Identity.Name != post.User.Nickname)
                return RedirectToAction("Index"); // Custom error page

            //EditPostViewModel model = postService.GetById(id).ToMvcEditPost();
            EditPostViewModel model = post.ToMvcEditPost();

            if (model == null)
                return HttpNotFound();

            model.TagList = new SelectList(tagService.GetAll().Select(tag => tag.ToMvcTag().Name));

            return View(model);
        }

        //[Authorize(Roles = "admin")]
        // Редактировать пост может только его сощдатель.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditPostViewModel model, string[] namesOfTags)
        {
            postService.Update(model.ToBllPost(namesOfTags));
            return RedirectToAction("Index");
        } 
        #endregion

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if(id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var post = postService.GetById((int)id)?.ToMvcDetailsPost();

            if (post == null)
            {
                Response.StatusCode = 404;
                return RedirectToAction("NotFound", "Error");
            }
                
            return View(post);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Like(int id)
        {
            postService.Like(new LikeEntity { PostId = id,
                                              UserId = userService.GetUserEntityByNickname(User.Identity.Name).Id});

            return Json(postService.GetById(id).Likes.Count);
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddCommentViaAjax(int postId, string text)
        {
            postService.AddComment(new CommentEntity
            {
                PublishDate = DateTime.Now,
                Text = text,
                Post = new PostEntity { Id = postId },
                User = new UserEntity { Id = userService.GetUserEntityByNickname(User.Identity.Name).Id }
            });

            return Json(commentService.GetCommentsByPostId(postId).OrderByDescending(c => c.Id).FirstOrDefault(c => c.User.Id == userService.GetUserEntityByNickname(User.Identity.Name).Id)?.ToMvcComment());
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddComment(int postId, string text) 
        {
            postService.AddComment(new CommentEntity {
                PublishDate = DateTime.Now,
                Text = text,
                Post = new PostEntity { Id = postId },
                User = new UserEntity { Id = userService.GetUserEntityByNickname(User.Identity.Name).Id }
            });

            return RedirectToAction("Details/" + postId);
        }

        [Authorize]
        [HttpPost]
        public ActionResult DeleteComment(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var comment = commentService.GetById((int)id);

            // Add filter
            if (User.Identity.Name != comment.User.Nickname || !User.IsInRole("admin"))
                return RedirectToAction("Login");

                if (comment == null)
                return HttpNotFound();
            
            commentService.Delete(comment);
            return RedirectToAction("Details/" + comment.Post.Id);
        }

        [Authorize]
        [HttpPost]
        public ActionResult DeleteCommentViaAjax(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var comment = commentService.GetById((int)id);

            var currentUserName = User.Identity.Name;
            var authorName = comment.User.Nickname;
            // Add filter
            if (User.Identity.Name != comment.User.Nickname || User.IsInRole("admin"))
                return RedirectToAction("Login");

            if (comment == null)
                return HttpNotFound();

            commentService.Delete(comment);
            return Json(true);
            //return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        public ActionResult SearchByTag(string tagname)
        {
            if(tagname == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            ViewBag.TagName = tagname;
            return View(postService.GetPostsByTagName(tagname)?.Select(post => post.ToMvcPost()));
        }

        #region Private fields
        private readonly IPostService postService;
        private readonly ITagService tagService;
        private readonly IUserService userService;
        private readonly ICommentService commentService; 
        #endregion
    }
}