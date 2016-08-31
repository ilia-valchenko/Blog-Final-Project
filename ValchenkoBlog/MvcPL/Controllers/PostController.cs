using System;
using System.Linq;
using System.Web.Mvc;
using BLL.Interfacies.Entities;
using BLL.Interfacies.Services;
using MvcPL.Infrastructure.Mappers;
using MvcPL.Models.Post;

namespace MvcPL.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService postService;

        public PostController(IPostService service)
        {
            postService = service;
        }

        public ActionResult Index()
        {
            return View(postService.GetAll().Select(post => post.ToMvcPost()));
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreatePostViewModel createPostViewModel)
        {
            postService.Create(createPostViewModel.ToBllPost());
            return RedirectToAction("Index");
        }

        //[Authorize(Roles = "Admin")]
        public ActionResult Delete(int postId)
        {
            var post = postService.GetById(postId);
            postService.Delete(post);
            return RedirectToAction("Index");
        }

        /*public ActionResult Like(int photoId, string name)
        {
            var photo = _service.GetById(photoId);
            if (photo.Likes.FirstOrDefault(l => l.UserName == User.Identity.Name) != null)
            {
                _service.RemoveLike(new LikeEntity
                {
                    PhotoId = photoId,
                    UserName = User.Identity.Name
                });
            }
            else
            {
                _service.AddLike(new LikeEntity
                {
                    PhotoId = photoId,
                    UserName = User.Identity.Name
                });
            }
            photo = _service.GetById(photoId);
            return PartialView("Like", photo.ToMvcPhoto());
        }*/

        // Add comment

        // Remove comment

        // Find by tag
    }
}