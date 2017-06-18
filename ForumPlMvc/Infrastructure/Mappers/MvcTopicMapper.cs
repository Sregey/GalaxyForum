using ForumPlMvc.Models;
using ForumBll.Interface.Models;
using System.Linq;
using System;

namespace ForumPlMvc.Infrastructure.Mappers
{
    public static class MvcTopicMapper
    {
        public static TopicListModel ToTopicListModel(this BllTopic bllTopic)
        {
            return new TopicListModel
            {
                Id = bllTopic.Id,
                Title = bllTopic.Title,
                AuthorLogin = bllTopic.Author?.Login,
                Date = bllTopic.Date,
                IsAnswered = bllTopic.IsAnswered,
                SectionName = bllTopic.Section.Name,
                SectionId = bllTopic.Section.Id,
                Status = bllTopic.Status.Name,
            };
        }

        public static TopicDitailsModel ToTopicDitailsModel(this BllTopic bllTopic)
        {
            return new TopicDitailsModel
            {
                Id = bllTopic.Id,
                Title = bllTopic.Title,
                Author = bllTopic.Author?.ToShortUserModel(),
                Date = bllTopic.Date,
                IsAnswered = bllTopic.IsAnswered,
                Text = bllTopic.Text,
                Comments = bllTopic.Comments.Select(c => c.ToCommentModel()),
            };
        }

        public static BllTopic ToBllTopic(this CreateEditTopicModel topic)
        {
            return new BllTopic
            {
                Id = topic.Id,
                //Author = new BllUser { Id = topic.AuthorId },
                Title = topic.Title,
                Text = topic.Text,
                Section = new BllSection() { Id = Int32.Parse(topic.Section) },
            };
        }

        public static CreateEditTopicModel ToCreateEditTopicModel(this BllTopic bllTopic)
        {
            return new CreateEditTopicModel
            {
                Id = bllTopic.Id,
                //AuthorId = bllTopic.Author.Id,
                Title = bllTopic.Title,
                Text = bllTopic.Text,
                Section = bllTopic.Section.Id.ToString(),
            };
        }
    }
}