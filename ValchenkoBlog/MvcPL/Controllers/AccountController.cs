using System;
using System.Drawing.Imaging;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using BLL.Interfacies.Entities;
using BLL.Interfacies.Services;
using MvcPL.Infrastructure;
using MvcPL.Infrastructure.Mappers;
using MvcPL.Models;
using MvcPL.Models.User;
using MvcPL.Models.Account;
using MvcPL.Providers;
using System.ComponentModel.DataAnnotations;

namespace MvcPL.Controllers
{
    //[Authorize]
    public class AccountController : Controller
    {
        private readonly IUserService userService;

        public AccountController(IUserService userService)
        {
            this.userService = userService;
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel loginViewModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(loginViewModel.Nickname, loginViewModel.Password))
                {
                    FormsAuthentication.SetAuthCookie(loginViewModel.Nickname, loginViewModel.RememberMe);
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "User");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect login or password.");
                }
            }
            ViewBag.returnUrl = returnUrl;
            return View(loginViewModel);


            ///////////
            /*if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный пароль или логин");
                }
            }
            return View(model);*/

            ///////////
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
        public ActionResult Register(RegisterViewModel registerViewModel)
        {
            if (registerViewModel.Captcha != (string)Session[CaptchaImage.CaptchaValueKey])
            {
                ModelState.AddModelError("Captcha", "Incorrect input.");
                return View(registerViewModel);
            }

            if (userService.GetOneByPredicate(u => u.Email == registerViewModel.Email) != null)
            {
                ModelState.AddModelError("", "User with this address already registered.");
                return View(registerViewModel);
            }
            if (userService.GetOneByPredicate(u => u.Nickname == registerViewModel.Nickname) != null)
            {
                ModelState.AddModelError("", "User with this nickname already registered.");
                return View(registerViewModel);
            }

            /*UserViewModel anyUser;
            try
            {
                anyUser = userService.GetUserEntityByEmail(registerViewModel.Email).ToMvcUser();
                ModelState.AddModelError("", "User with this email already registered.");
                return View(registerViewModel);
            }
            catch (ValidationException ex)
            { }*/

            //if (anyUser!=null)
            //{
            //    ModelState.AddModelError("", "User with this address already registered.");
            //    return View(viewModel);
            //}

            if (ModelState.IsValid)
            {
                var membershipUser = ((CustomMembershipProvider)Membership.Provider)
                    .CreateUser(registerViewModel.Email, registerViewModel.Nickname, registerViewModel.Password);

                if (membershipUser != null)
                {
                    FormsAuthentication.SetAuthCookie(registerViewModel.Email, false);
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Error registration.");
                }
            }
            return View(registerViewModel);
        }

        [AllowAnonymous]
        public ActionResult Captcha()
        {
            Session[CaptchaImage.CaptchaValueKey] =
                new Random(DateTime.Now.Millisecond).Next(1111, 9999).ToString(CultureInfo.InvariantCulture);
            var ci = new CaptchaImage(Session[CaptchaImage.CaptchaValueKey].ToString(), 211, 50, "Helvetica");

            // Change the response headers to output a JPEG image.
            this.Response.Clear();
            this.Response.ContentType = "image/jpeg";

            // Write the image to the response stream in JPEG format.
            ci.Image.Save(this.Response.OutputStream, ImageFormat.Jpeg);

            // Dispose of the CAPTCHA image object.
            ci.Dispose();
            return null;
        }

        [ChildActionOnly]
        public ActionResult LoginPartial()
        {
            return PartialView("_LoginPartial");
        }

    }
}