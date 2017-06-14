using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ForumBll.Interface.Services;
using ForumBll.Interface.Models;
using ForumPlMvc.Infrastructure.Mappers;
using ForumPlMvc.Models;

namespace ForumPlMvc.Controllers
{
    public class HomeController : Controller
    {
        private const int TOPICS_PER_PAGE = 1;

        private readonly IUserService userService;
        private readonly ISectionService sectionService;
        private readonly ITopicService topicService;
        private readonly IImageService imageService;

        public HomeController(IUserService userService,
            ISectionService sectionService,
            ITopicService topicService,
            IImageService imageService)
        {
            this.userService = userService;
            this.sectionService = sectionService;
            this.topicService = topicService;
            this.imageService = imageService;
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
            return View(users.Select(bllUser => bllUser.ToUserInfoModel()));
        }

        public FileStreamResult GetImage(int? id)
        {
            if (id.HasValue)
            {
                BllImage image = imageService.GetImage(id.Value);
                return new FileStreamResult(image.Content, "image//" + image.Content);
            }
            return null;

            //DateTime t;
            //t.Ho
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
                //BllSection section;
                //section.

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
                    .Select(bllTopic => bllTopic.ToTopicListModel()));
            }
            return View("Error");
        }

        public ActionResult Topic(int? id)
        {
            if (id.HasValue)
            {
                return View(topicService.GetTopic(id.Value).ToTopicDitailsModel());
            }
            return View("Error");
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