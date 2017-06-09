using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ForumBll.Interface.Services;
using ForumBll.Interface.Models;
using GalaxyForum.Infrastructure.Mappers;

namespace GalaxyForum.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService userService;
        private readonly ISectionService sectionService;

        public HomeController(IUserService userService, ISectionService sectionService)
        {
            this.userService = userService;
            this.sectionService = sectionService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Users()
        {
            IEnumerable<BllUser> users = userService.GetAllUsers();
            //List<BllUser> list = users.ToList();
            return View(users.Select(bllUser => bllUser.ToMvcUser()));
        }

        public ActionResult MainPage()
        {
            return View();
        }

        public ActionResult Sections()
        {
            IEnumerable<BllSection> sections = sectionService.GetAllSections();
            return View(sections.Select(bllSection => bllSection.ToMvcSection()));
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}