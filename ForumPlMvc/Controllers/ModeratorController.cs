using System.Web.Mvc;
using System.Web.Security;
using System.Linq;
using ForumBll.Interface.Services;
using ForumBll.Interface.Models;
using ForumPlMvc.Infrastructure.Mappers;
using ForumPlMvc.Models;
using ForumPlMvc.Models.Filters;
using ForumPlMvc.Infrastructure;

namespace ForumPlMvc.Controllers
{
    [Authorize(Roles = "Moderator,Admin")]
    public class ModeratorController : Controller
    {
        private const int TOPICS_PER_PAGE = 2;

        private readonly ITopicService topicService;

        public ModeratorController(ITopicService topicService)
        {
            this.topicService = topicService;
        }

        public ActionResult _Topics(int? page)
        {
            ViewBag.IsShowStatus = true;
            var topics = topicService.GetTopics();
            topics = topics.OrderBy(t => t.Date, new DateComparer());
            return PartialView(this.GetItemsOnPage(topics, page, TOPICS_PER_PAGE)
                .Select(bllTopic => bllTopic.ToTopicListModel()));
        }

        public ActionResult EditTopic(int? id)
        {
            if (id.HasValue)
            {
                BllTopic bllTopic = topicService.GetTopic(id.Value);
                bllTopic.Status = new BllStatus { Id = (int)StatusEnum.Processed };
                topicService.UpdateTopic(bllTopic);
                CreateEditTopicModel topic = bllTopic.ToCreateEditTopicModel();
                topic.Sections = topicService.GetAllSections().Select(s => s.Name);
                return View(topic);
            }
            return View("Error");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [MultipleSubmit(Name = "Action", Argument = "Accept")]
        public ActionResult AcceptTopic(CreateEditTopicModel topic)
        {
            return ChangeTopicState(topic, StatusEnum.Accepted);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [MultipleSubmit(Name = "Action", Argument = "Reject")]
        public ActionResult RejectTopic(CreateEditTopicModel topic)
        {
            return ChangeTopicState(topic, StatusEnum.Rejected);
        }

        public ActionResult Topics(int? page)
        {
            ViewBag.IsShowStatus = true;
            var topics = topicService.GetTopics();
            topics = topics.OrderBy(t => t.Date, new DateComparer());
            return View("Topics", this.GetItemsOnPage(topics, page, TOPICS_PER_PAGE)
                .Select(bllTopic => bllTopic.ToTopicListModel()));
        }

        public ActionResult GetRawTopic()
        {
            return RedirectToAction("EditTopic", new { id = topicService.GetRawTopic().Id });
        }

        protected override void Dispose(bool disposing)
        {
            topicService.Dispose();
            base.Dispose(disposing);
        }

        private ActionResult ChangeTopicState(CreateEditTopicModel topic, StatusEnum status)
        {
            if (ModelState.IsValid)
            {
                BllTopic bllTopic = topic.ToBllTopic();
                bllTopic.Status = new BllStatus { Id = (int)status };
                topicService.UpdateTopic(bllTopic);
                return RedirectToAction("Topic", "Home", new { id = topic.Id });
            }
            return View("EditTopic", topic);
        }
    }
}