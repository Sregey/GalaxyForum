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

namespace ForumPlMvc.Controllers
{
    [Authorize(Roles = "Moderator,Admin")]
    public class ModeratorController : Controller
    {
        private const int TOPICS_PER_PAGE = 2;
        private const int COMMENTS_PER_PAGE = 2;

        private readonly ITopicService topicService;
        private readonly ICommentService commentService;

        public ModeratorController(ITopicService topicService,
            ICommentService commentService)
        {
            this.topicService = topicService;
            this.commentService = commentService;
        }

        public ActionResult _Topics(int? page)
        {
            ViewBag.IsShowStatus = true;
            var topics = topicService.GetTopics();
            topics = topics.OrderBy(t => t.Date, new DateComparer());
            return PartialView(this.GetItemsOnPage(topics, page, TOPICS_PER_PAGE)
                .Select(bllTopic => bllTopic.ToTopicListModel()));
        }

        [IdValidator]
        public ActionResult EditTopic(int id)
        {
            BllTopic bllTopic = topicService.GetTopic(id);
            bllTopic.Status = new BllStatus { Id = (int)StatusEnum.Processed };
            topicService.UpdateTopic(bllTopic);
            CreateEditTopicModel topic = bllTopic.ToCreateEditTopicModel();
            topic.Sections = topicService.GetAllSections()
                .Select(s => new SelectListItem { Text = s.Name, Value = s.Id.ToString() });
            return View(topic);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [MultipleSubmit(Name = "EditTopic", Argument = "Accept")]
        public ActionResult AcceptTopic(CreateEditTopicModel topic)
        {
            return ChangeTopicState(topic, StatusEnum.Accepted);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [MultipleSubmit(Name = "EditTopic", Argument = "Reject")]
        public ActionResult RejectTopic(CreateEditTopicModel topic)
        {
            return ChangeTopicState(topic, StatusEnum.Rejected);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [MultipleSubmit(Name = "EditTopic", Argument = "Delete")]
        public ActionResult DeletTopic(CreateEditTopicModel topic)
        {
            topicService.DeleteTopic(topic.Id);
            return RedirectToAction("Topics");
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
            BllTopic topic = topicService.GetRawTopic();
            if (topic != null)
                return RedirectToAction("EditTopic", new { id = topic.Id });
            else
                return View("Info", (object)"All topics are processed.");
        }

        public ActionResult GetRawComment()
        {
            BllComment comment = commentService.GetRawComment();
            if (comment != null)
                return RedirectToAction("EditComment", new { id = comment.Id });
            else
                return View("Info", (object)"All comments are processed.");

            
        }

        public ActionResult Comments(int? page)
        {
            ViewBag.IsMyTopic = false;
            var comments = commentService.GetComments();
            comments = comments.OrderBy(t => t.Date);
            return View(this.GetItemsOnPage(comments, page, COMMENTS_PER_PAGE)
                .Select(c => c.ToCommentModel()));
        }

        [IdValidator]
        public ActionResult EditComment(int id)
        {
            return View(commentService.GetComment(id).ToAddEditCommentModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [MultipleSubmit(Name = "EditComment", Argument = "Accept")]
        public ActionResult AcceptComment(AddEditCommentModel comment)
        {
            return ChangeCommentState(comment, StatusEnum.Accepted);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [MultipleSubmit(Name = "EditComment", Argument = "Reject")]
        public ActionResult RejectComment(AddEditCommentModel comment)
        {
            return ChangeCommentState(comment, StatusEnum.Rejected);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [MultipleSubmit(Name = "EditComment", Argument = "Delete")]
        public ActionResult DeleteComment(AddEditCommentModel comment)
        {
            commentService.DeleteComment(comment.Id);
            return RedirectToAction("Topic", "Home", new { id = comment.TopicId });
        }

        public ActionResult _Comments(int? page)
        {
            ViewBag.IsMyTopic = false;
            var comments = commentService.GetComments();
            comments = comments.OrderBy(t => t.Date);
            return PartialView(this.GetItemsOnPage(comments, page, TOPICS_PER_PAGE)
                .Select(c => c.ToCommentModel()));
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

        private ActionResult ChangeCommentState(AddEditCommentModel comment, StatusEnum status)
        {
            if (ModelState.IsValid)
            {
                BllComment bllComment = comment.ToBllComment();
                bllComment.Status = new BllStatus { Id = (int)status };
                commentService.UpdateComment(bllComment);
                return RedirectToAction("Topic", "Home", new { id = comment.TopicId });
            }
            return View("EditComment", comment);
        }
    }
}