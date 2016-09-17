using System.Web;
using System.Linq;
using System.Web.Mvc;
using BLL.Interfacies.Entities;
using BLL.Interfacies.Services;
using MvcPL.Infrastructure.Mappers;
using MvcPL.Models.User;

namespace MvcPL.Controllers
{
    public class UserController : Controller
    {
        public UserController(IUserService userService, IRoleService roleService)
        {
           this.userService = userService;
           this.roleService = roleService;
        }

        public ActionResult Index() => View(userService.GetAll().Select(user => user.ToMvcUser()));

        //GET-запрос к методу Delete несет потенциальную уязвимость!
        [HttpGet]
        public ActionResult Delete(int id = 0)
        {
            UserEntity user = userService.GetById(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user.ToMvcUser());
        }

        //Post/Redirect/Get (PRG) — модель поведения веб-приложений, используемая
        //разработчиками для защиты от повторной отправки данных веб-форм
        //(Double Submit Problem)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(UserEntity user)
        {
            userService.Delete(user);
            return RedirectToAction("Index");
        }

        //etc.

        [HttpGet]
        [AllowAnonymous]
        public ActionResult UserProfile(string nickname)
        {
            if (nickname == null)
                return RedirectToAction("BadRequest", "Error");

            var user = userService.GetUserEntityByNickname(nickname);
            var model = user?.ToMvcUser();

            if (model == null)
                return RedirectToAction("NotFound", "Error");

            var roles = roleService.GetRolesOfUser(user.Id);

            if (roles != null)
                foreach (var role in roles)
                    model.Roles.Add(role.Name);

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public ActionResult ChangeAvatar(HttpPostedFileBase file)
        {
            if(file == null)
                return RedirectToAction("BadRequest", "Error");

            // throw an exception
            if (file.ContentLength < 0)
                return RedirectToAction("Error", "Error");

            userService.ChangeAvatar(User.Identity.Name, file);

            return RedirectToAction("UserProfile", "User", new { nickname = User.Identity.Name });
        }

        private readonly IUserService userService;
        private readonly IRoleService roleService;
    }
}