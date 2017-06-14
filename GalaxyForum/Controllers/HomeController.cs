using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ForumBll.Interface.Services;
using ForumBll.Interface.Models;
using GalaxyForum.Infrastructure.Mappers;
using GalaxyForum.Models;

namespace GalaxyForum.Controllers
{
    public class HomeController : Controller
    {
        private const int TOPICS_PER_PAGE = 1;

        private readonly IUserService userService;
        private readonly ISectionService sectionService;
        private readonly ITopicService topicService;

        public HomeController(IUserService userService,
            ISectionService sectionService,
            ITopicService topicService)
        {
            this.userService = userService;
            this.sectionService = sectionService;
            this.topicService = topicService;
        }

        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
                ViewBag.UserName = User.Identity.Name;
            return View();
        }

        [Authorize(Roles = "Admin")]
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

        [Authorize]
        public ActionResult Sections()
        {
            return View(sectionService
                .GetAllSections()
                .Select(bllSection => bllSection.ToMvcSection()));
        }

        public ActionResult TopicsInSection(int? id, int? page)
        {
            if (id.HasValue)
            {
                int topicCount = topicService.GetTopicCountInSection(id.Value);
                int pageCount = (int)Math.Ceiling(topicCount / (double)TOPICS_PER_PAGE);
                if (!page.HasValue || (page.Value <= 0) || (page.Value > pageCount))
                    page = 1;

                ViewBag.Page = page.Value;
                ViewBag.PageCount = pageCount;

                return View(topicService
                    .GetTopicsFromSection(
                        id.Value, 
                        (page.Value - 1) * TOPICS_PER_PAGE,
                        TOPICS_PER_PAGE)
                    .Select(bllTopic => bllTopic.ToMvcTopic()));
            }
            return View("Error");
        }

        public ActionResult CreateTopic()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateTopic(MvcTopic topic)
        {
            if (ModelState.IsValid)
            {
                return Redirect("Section\\" + 3);
            }
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