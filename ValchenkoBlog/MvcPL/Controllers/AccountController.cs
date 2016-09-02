using System.Web.Mvc;
using System.Web.Security;
using BLL.Interfacies.Services;
using MvcPL.Models.User;
using MvcPL.Models.Account;
using MvcPL.Providers;

namespace MvcPL.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IUserService _model;

        public AccountController(IUserService model)
        {
            _model = model;
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            var type = HttpContext.User.GetType();
            var iden = HttpContext.User.Identity.GetType();
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel viewModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(viewModel.Nickname, viewModel.Password))
                //Проверяет учетные данные пользователя и управляет параметрами пользователей
                {
                    FormsAuthentication.SetAuthCookie(viewModel.Nickname, viewModel.RememberMe);
                    //Управляет службами проверки подлинности с помощью форм для веб-приложений
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Index", "User");
                }
                ModelState.AddModelError("", "Incorrect login or password.");
            }
            return View(viewModel);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel viewModel)
        {
            if (_model.GetOneByPredicate(u => u.Nickname == viewModel.Nickname) != null)
            {
                ModelState.AddModelError("", "User with this nickname already registered.");
                return View(viewModel);
            }

            if (ModelState.IsValid)
            {
                var membershipUser = ((CustomMembershipProvider)Membership.Provider)
                    .CreateUser(viewModel.Nickname, viewModel.Password);

                if (membershipUser != null)
                {
                    FormsAuthentication.SetAuthCookie(viewModel.Nickname, false);
                    return RedirectToAction("Index", "User");
                }
                ModelState.AddModelError("", "Error registration.");
            }
            return View(viewModel);
        }


        [ChildActionOnly]
        public ActionResult LoginPartial()
        {
            return PartialView("_LoginPartial");
        }
    }
}