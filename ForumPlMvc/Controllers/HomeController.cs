using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ForumBll.Interface.Services;
using ForumBll.Interface.Models;
using ForumPlMvc.Infrastructure.Mappers;
using ForumPlMvc.Filters;

namespace ForumPlMvc.Controllers
{
    public class HomeController : Controller
    {
        private const int TOPICS_PER_PAGE = 1;
        private const int COMMENTS_PER_PAGE = 2;

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

        [IdValidator]
        public FileStreamResult GetImage(int id)
        {
            BllImage image = imageService.GetImage(id);
            return new FileStreamResult(image.Content, "image//" + image.Content);
        }

        public ActionResult Sections()
        {
            return View(sectionService
                .GetAllSections()
                .Select(bllSection => bllSection.ToSectionModel()));
        }

        [IdValidator]
        public ActionResult TopicsInSection(int id, int? page)
        {
            ViewBag.AjaxId = id;
            ViewBag.IsShowStatus = false;
            ViewBag.SectionId = id;

            BllSection section = sectionService.GetSection(id);
            section.Topics = section.Topics.Where(t => t.Status.Id == (int)StatusEnum.Accepted);
            return View("Topics", this.GetItemsOnPage(section.Topics, page, TOPICS_PER_PAGE)
                .Select(bllTopic => bllTopic.ToTopicListModel()));
        }

        //[IdValidator]
        //public ActionResult TopicsInSection(int id, int? page, string subString)
        //{
        //    ViewBag.AjaxId = id;
        //    ViewBag.IsShowStatus = false;

        //    BllSection section = sectionService.GetSection(id);
        //    section.Topics = section.Topics.Where(t => t.Status.Id == (int)StatusEnum.Accepted);
        //    section.Topics = topicService.SearchInSection(id, "Car");
        //    return View("Topics", this.GetItemsOnPage(section.Topics, page, TOPICS_PER_PAGE)
        //        .Select(bllTopic => bllTopic.ToTopicListModel()));
        //}

        [IdValidator]
        public ActionResult Topic(int id, int? page)
        {
            BllTopic topic = GetTopic(id, page);
            ViewBag.IsMyTopic = IsMyTopic(topic.Id);
            return View(topic.ToTopicDitailsModel());
        }

        [IdValidator]
        public ActionResult _Topics(int id, int? page)
        {
            ViewBag.IsShowStatus = false;
            BllSection section = sectionService.GetSection(id);
            return PartialView(this.GetItemsOnPage(section.Topics, page, TOPICS_PER_PAGE)
                .Select(bllTopic => bllTopic.ToTopicListModel()));
        }

        [IdValidator]
        public ActionResult _Comments(int id, int? page)
        {
            BllTopic topic = GetTopic(id, page);
            ViewBag.IsMyTopic = IsMyTopic(topic.Id);
            return PartialView(topic.Comments
                .Select(c => c.ToCommentModel()));
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            userService.Dispose();
            sectionService.Dispose();
            topicService.Dispose();
            imageService.Dispose();

            base.Dispose(disposing);
            GC.SuppressFinalize(this);
        }

        private BllTopic GetTopic(int id, int? page)
        {
            BllTopic topic = topicService.GetTopic(id);

            topic.Comments = this.GetItemsOnPage(
                topic.Comments.OrderBy(c => c.Date), page, COMMENTS_PER_PAGE);

            return topic;
        }

        private bool IsMyTopic(int topicId)
        {
            bool isMyTopic = false;
            if (User.Identity.IsAuthenticated)
            {
                BllUser user = userService.GetUser(User.Identity.Name);
                isMyTopic = user.Id == topicId;
            }
            return isMyTopic;
        }
    }
}
