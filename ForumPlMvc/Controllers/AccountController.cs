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
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel newUser)
        {
            if (ModelState.IsValid)
            {
                if (accountService.RegisterUser(newUser.ToBllUser()))
                {
                    FormsAuthentication.SetAuthCookie(newUser.Login, true);
                    return RedirectToAction("UserInfo", "User");
                }
            }
            return View(newUser);
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
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

        [AllowAnonymous]
        public JsonResult CheckLogin(string login, int? id)
        {
            bool result = false;
            if (!String.IsNullOrEmpty(login))
            {
                if (id.HasValue)
                {
                    result = !accountService.IsLoginExist(login, id.Value);
                }
                else
                {
                    result = !accountService.IsLoginExist(login);
                }
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public JsonResult CheckEmail(string email)
        {
            return Json(!accountService.IsEmailExist(email),
                JsonRequestBehavior.AllowGet);
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

        protected override void Dispose(bool disposing)
        {
            accountService.Dispose();
            base.Dispose(disposing);
            GC.SuppressFinalize(this);
        }
    }
}
