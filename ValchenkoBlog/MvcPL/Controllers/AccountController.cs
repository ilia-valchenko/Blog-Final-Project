using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcPL.Models;
using MvcPL.Infrastructure.Mappers;
using BLL.Interfacies.Entities;
using BLL.Interfacies.Services;
using MvcPL.Models.User;
using MvcPL.Providers;
using System.Web.Security;

namespace MvcPL.Controllers
{
    public class AccountController : Controller
    {
        public AccountController(IUserService userService)
        {
            this.userService = userService;
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            var type = HttpContext.User.GetType();
            var iden = HttpContext.User.Identity.GetType();
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        /*[HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LogOnViewModel viewModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(viewModel.Name, viewModel.Password))
                //Проверяет учетные данные пользователя и управляет параметрами пользователей
                {
                    FormsAuthentication.SetAuthCookie(viewModel.Name, viewModel.RememberMe);
                    //Управляет службами проверки подлинности с помощью форм для веб-приложений
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Incorrect login or password.");
            }
            return View(viewModel);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login", "Account");
        }*/

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
            if (userService.GetOneByPredicate(u => u.Nickname == viewModel.Nickname) != null)
            {
                ModelState.AddModelError("", "User with this nickname already registered.");
                return View(viewModel);
            }
            if (userService.GetOneByPredicate(u => u.Nickname == viewModel.Nickname) != null)
            {
                ModelState.AddModelError("", "User with this name already registered.");
                return View(viewModel);
            }

            if (ModelState.IsValid)
            {
                var membershipUser = ((CustomMembershipProvider)Membership.Provider)
                    .CreateUser(viewModel.Nickname, viewModel.Password);

                if (membershipUser != null)
                {
                    FormsAuthentication.SetAuthCookie(viewModel.Nickname, false);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Error registration.");
            }
            return View(viewModel);
        }

        /*[ChildActionOnly]
        public ActionResult LoginPartial()
        {
            return PartialView("_LoginPartial");
        }*/

        // FROM DEMO PROJECT

        [ActionName("Index")]
        public ActionResult GetAllUsers() => View(userService.GetAll().Select(user => user.ToMvcUser()));

        [HttpGet]
        public ActionResult Create(UserViewModel mvcUser)
        {
            userService.Create(mvcUser?.ToBllUser());
            return RedirectToAction("Index");
        }

        //GET-запрос к методу Delete несет потенциальную уязвимость!
        [HttpGet]
        public ActionResult Delete(int id = 0)
        {
            UserEntity user = userService.GetById(id);

            if (user == null)
                return HttpNotFound();

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

        private readonly IUserService userService;
    }
}