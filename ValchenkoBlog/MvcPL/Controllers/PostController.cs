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

        // НЕСЕТ ПОТЕНЦИАЛЬНУЮ УЯЗВИМОСТЬ!
        // GET: Posts/Delete/5
        //[Authorize(Roles = "Admin")]
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

        public ActionResult SearchByTag(string tagname)
        {
            //var posts = new List<PostViewModel>();
            ViewBag.TagName = tagname;

            /*var post = new PostViewModel()
            {
                Id = 111,
                Title = "I found this post by tag #lolipop",
                Description = "Go go forever.",
                PublishDate = "03-09-2016",
                Author = new Models.User.UserViewModel
                {
                    Id = 1,
                    Nickname = "Robin Hood"
                },
                Tags = new List<TagViewModel>(),
                NumberOfComments = 0,
                NumberOfLikes = 100
            };*/

            

            //posts.Add(post);

            return View(postService.GetPostsByTagName(tagname).Select(post => post.ToMvcPost()));
        }

        private readonly IPostService postService;
        private readonly ITagService tagService;
        private readonly IUserService userService;
    }
}