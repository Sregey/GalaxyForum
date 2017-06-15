using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ForumBll.Interface.Services;
using ForumBll.Interface.Models;
using ForumPlMvc.Infrastructure.Mappers;
using ForumPlMvc.Models;


namespace ForumPlMvc.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel newUser)
        {
            if (ModelState.IsValid)
            {
                if (!accountService.IsLoginOrEmailExist(newUser.Login, newUser.Email))
                {
                    if (accountService.RegisterUser(newUser.ToBllUser()))
                    {
                        FormsAuthentication.SetAuthCookie(newUser.Login, true);
                        return RedirectToAction("UserInfo", "User");
                    }
                }
                else
                    ModelState.AddModelError("", "This login or email already exists.");
            }
            return View(newUser);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                BllUser user = accountService.Login(loginModel.Email, loginModel.Password);
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(user.Login, true);
                    return RedirectToAction("UserInfo", "User");
                }
                else
                    ModelState.AddModelError("", "You input yuor email or password wrong.");
            }
            return View(loginModel);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordModel pswModel)
        {
            if (ModelState.IsValid)
            {
                if (accountService.ChangePassword(User.Identity.Name, pswModel.OldPassword, pswModel.NewPassword))
                {
                    return RedirectToAction("UserInfo", "User");
                }
                else
                    ModelState.AddModelError("", "You input incorrect old password.");
            }
            return View();
        }
    }
}