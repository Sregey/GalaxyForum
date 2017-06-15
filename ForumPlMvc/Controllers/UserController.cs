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

        //public ActionResult UserInfo()
        //{
        //    return View(userService.GetUser(User.Identity.Name).ToUserInfoModel());
        //}

        public ActionResult Topics(int? page)
        {
            BllUser user = userService.GetUser(User.Identity.Name);
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
            var topic = new CreateTopicModel();
            topic.Sections = topicService.GetAllSections().Select(s => s.Name);
            return View(topic);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTopic(CreateTopicModel topic)
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
        public ActionResult Comment(AddCommentModel comment)
        {
            if (ModelState.IsValid)
            {
                BllComment bllComment = comment.ToBllComment();
                bllComment.Sender = userService.GetUser(User.Identity.Name);
                commentService.AddComment(bllComment);

                return RedirectToAction("Topic", "Home", new { id = comment.TopicId });
            }
            return View(comment);
        }
    }
}