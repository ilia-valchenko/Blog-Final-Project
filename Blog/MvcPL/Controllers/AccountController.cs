using System;
using System.Drawing.Imaging;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using BLL.Interfacies.Services;
using MvcPL.Infrastructure;
using MvcPL.Models.Account;
using MvcPL.Providers;

namespace MvcPL.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService userService;

        public AccountController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
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
                        return RedirectToAction("Index", "Post");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect login or password.");
                }
            }

            ViewBag.returnUrl = returnUrl;
            return View(loginViewModel);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel registerViewModel)
        {
            if (registerViewModel.Captcha != (string)Session[CaptchaImage.CaptchaValueKey])
            {
                ModelState.AddModelError("Captcha", "Incorrect input.");
                return View(registerViewModel);
            }

            if (userService.GetUserEntityByEmail(registerViewModel.Email) != null)
            {
                ModelState.AddModelError("", "User with this email already registered.");
                return View(registerViewModel);
            }

            if (userService.GetUserEntityByNickname(registerViewModel.Nickname) != null)
            {
                ModelState.AddModelError("", "User with this nickname already registered.");
                return View(registerViewModel);
            }

            if (ModelState.IsValid)
            {
                var membershipUser = ((CustomMembershipProvider)Membership.Provider).CreateUser(registerViewModel.Email,
                                                                                                registerViewModel.Nickname,
                                                                                                registerViewModel.Password);

                if (membershipUser != null)
                {
                    FormsAuthentication.SetAuthCookie(registerViewModel.Nickname, false);
                    return RedirectToAction("Index", "Post");
                }
                else
                {
                    ModelState.AddModelError("", "Error registration.");
                }
            }
            return View(registerViewModel);
        }

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
    }
}