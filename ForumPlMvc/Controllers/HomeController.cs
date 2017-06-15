using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ForumBll.Interface.Services;
using ForumBll.Interface.Models;
using ForumPlMvc.Infrastructure;
using ForumPlMvc.Infrastructure.Mappers;
using ForumPlMvc.Models;
using System.Diagnostics;

namespace ForumPlMvc.Controllers
{
    public class HomeController : Controller
    {
        private const int TOPICS_PER_PAGE = 1;
        private const int COMMENTS_PER_PAGE = 1;

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
        }

        public ActionResult MainPage()
        {
            return View();
        }

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
                BllSection section = sectionService.GetSection(id.Value);

                return View("Topics", this.GetItemsOnPage(section.Topics, page, TOPICS_PER_PAGE)
                    .Select(bllTopic => bllTopic.ToTopicListModel()));
            }
            return View("Error");
        }

        public ActionResult Topic(int? id, int? page)
        {
            if (id.HasValue)
            {
                return View(GetTopic(id.Value, page).ToTopicDitailsModel());
            }
            return View("Error");
        }

        public ActionResult _Comments(int? id, int? page)
        {
            if (id.HasValue)
            {
                return PartialView(GetTopic(id.Value, page)
                    .Comments
                    .Select(c => c.ToCommentModel()));
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

        private BllTopic GetTopic(int id, int? page)
        {
            BllTopic topic = topicService.GetTopic(id);

            topic.Comments = this.GetItemsOnPage(
                topic.Comments.OrderBy(c => c.Date, new DateComparer()), page, COMMENTS_PER_PAGE);

            return topic;
        }
    }
}