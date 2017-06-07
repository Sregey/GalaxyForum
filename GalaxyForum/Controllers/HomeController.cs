using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ForumBll.Interface.Services;
using ForumBll.Interface.Models;

namespace GalaxyForum.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService service;

        public HomeController(IUserService service)
        {
            this.service = service;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MainPage()
        {
            BllUser user = service.GetUserEntity(1);
            return View();
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