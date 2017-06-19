using System.Web.Mvc;
using System.Web.Security;
using System.Linq;
using ForumBll.Interface.Services;
using ForumBll.Interface.Models;
using ForumPlMvc.Infrastructure.Mappers;
using ForumPlMvc.Models;
using ForumPlMvc.Models.Filters;
using ForumPlMvc.Infrastructure;
using ForumPlMvc.Filters;
using System;

namespace ForumPlMvc.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private const int USERS_PER_PAGE = 3;

        private readonly IUserService userService;
        private readonly ISectionService sectionService;
        private readonly IRoleService roleService;

        public AdminController(IUserService userService,
            ISectionService sectionService,
            IRoleService roleService)
        {
            this.userService = userService;
            this.sectionService = sectionService;
            this.roleService = roleService;
        }

        public ActionResult Users(int? page)
        {
            var users = userService.GetAllUsers();
            return View(this.GetItemsOnPage(users, page, USERS_PER_PAGE)
                .Select(u => u.ToUserListModel()));
        }

        public ActionResult AddSection()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddSection(SectionModel section)
        {
            if (ModelState.IsValid)
            {
                sectionService.AddSection(section.ToBllSection());
                return RedirectToAction("Sections", "Home");
            }
            return View(section);
        }

        [IdValidator]
        public ActionResult DeleteSection(int id)
        {
            sectionService.DeleteSection(id);
            return RedirectToAction("Sections", "Home");
        }

        [IdValidator]
        public ActionResult EditSection(int id)
        {
            return View(sectionService.GetSection(id).ToSectionModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSection(SectionModel section)
        {
            if (ModelState.IsValid)
            {
                sectionService.UpdateSection(section.ToBllSection());
                return RedirectToAction("Sections", "Home");
            }
            return View(section);
        }

        [IdValidator]
        public ActionResult DeleteUser(int id)
        {
            userService.DeleteUser(id);
            return RedirectToAction("Users");
        }

        [IdValidator]
        public ActionResult EditUser(int id)
        {
            var user = userService.GetUser(id).ToUserAdminEditModel();
            user.Roles = roleService.GetAllRoles()
                .Select(r => new SelectListItem { Text = r.Name, Value = r.Id.ToString()});
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser(UserAdminEditModel user)
        {
            if (ModelState.IsValid)
            {
                BllUser bllUser = userService.GetUser(user.Id);
                user.CopyToBllUser(bllUser);
                userService.UpdateUser(bllUser);
                return RedirectToAction("Users");
            }
            return View(user);
        }

        public ActionResult _Users(int? page)
        {
            var users = userService.GetAllUsers();
            return PartialView(this.GetItemsOnPage(users, page, USERS_PER_PAGE)
                .Select(u => u.ToUserListModel()));
        }

        protected override void Dispose(bool disposing)
        {
            userService.Dispose();

            base.Dispose(disposing);
            GC.SuppressFinalize(this);
        }
    }
}