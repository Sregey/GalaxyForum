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
using ForumPlMvc.Infrastructure;
using ForumPlMvc.Filters;

namespace ForumPlMvc.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private const int TOPICS_PER_PAGE = 1;

        private readonly IUserService userService;
        private readonly ICommentService commentService;
        private readonly ITopicService topicService;

        public UserController(IUserService userService, 
            ICommentService commentService,
            ITopicService topicService)
        {
            this.userService = userService;
            this.commentService = commentService;
            this.topicService = topicService;
        }

        public ActionResult Topics(int? page)
        {
            ViewBag.IsShowStatus = true;
            BllUser user = userService.GetUser(User.Identity.Name);
            user.Topics = user.Topics.OrderBy(t => t.Date, new DateComparer());
            return View("Topics", this.GetItemsOnPage(user.Topics, page, TOPICS_PER_PAGE)
                .Select(bllTopic => bllTopic.ToTopicListModel()));
        }

        public ActionResult UserInfo(int? id)
        {
            if (id.HasValue)
            {
                return View(userService.GetUser(id.Value).ToUserInfoModel());
            }
            else
                return View(userService.GetUser(User.Identity.Name).ToUserInfoModel());
        }

        public ActionResult Setting()
        {
            return View(userService
                .GetUser(User.Identity.Name)
                .ToUserSettingModel());
        }

        public ActionResult _Topics(int? page)
        {
            BllUser user = userService.GetUser(User.Identity.Name);
            ViewBag.IsShowStatus = true;
            user.Topics = user.Topics.OrderBy(t => t.Date, new DateComparer());
            return PartialView(this.GetItemsOnPage(user.Topics, page, TOPICS_PER_PAGE)
                .Select(bllTopic => bllTopic.ToTopicListModel()));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Setting(UserSettingModel newSetting)
        {
            if (ModelState.IsValid)
            {
                BllUser user = userService.GetUser(User.Identity.Name);
                newSetting.CopyToBllUser(user);
                userService.UpdateUser(user);
                return RedirectToAction("UserInfo");
            }

            return View(newSetting);
        }

        public ActionResult CreateTopic()
        {
            var topic = new CreateEditTopicModel();
            topic.Sections = topicService.GetAllSections()
                .Select(s => new SelectListItem { Text = s.Name, Value = s.Id.ToString() });
            return View(topic);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTopic(CreateEditTopicModel topic)
        {
            if (ModelState.IsValid)
            {
                BllTopic bllTopic = topic.ToBllTopic();
                bllTopic.Author = userService.GetUser(User.Identity.Name);
                topicService.AddTopic(bllTopic);
                return RedirectToAction("Topics");
            }
            return View(topic);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Comment(AddEditCommentModel comment)
        {
            if (ModelState.IsValid)
            {
                BllComment bllComment = comment.ToBllComment();
                bllComment.Sender = userService.GetUser(User.Identity.Name);
                commentService.AddComment(bllComment);
            }
            return RedirectToAction("Topic", "Home", new { id = comment.TopicId });
        }

        [IdValidator]
        public ActionResult MarkCommentAsGood(int id)
        {
            BllComment comment = commentService.GetComment(id);
            bool isCanMark = comment.Topic.Author.Id == userService.GetUser(User.Identity.Name).Id;
            isCanMark |= User.IsInRole("Admin") || User.IsInRole("Moderator");
            comment.IsAnswer = isCanMark;
            commentService.UpdateComment(comment);
            return RedirectToAction("Topic", "Home", new { Id = comment.Topic.Id });
        }

        protected override void Dispose(bool disposing)
        {
            userService.Dispose();
            commentService.Dispose();
            topicService.Dispose();

            base.Dispose(disposing);
            GC.SuppressFinalize(this);
        }
    }
}