﻿using ForumPlMvc.Models;
using ForumBll.Interface.Models;
using System.Linq;

namespace ForumPlMvc.Infrastructure.Mappers
{
    public static class MvcTopicMapper
    {
        //public static BllTopic ToBllTopic(this MvcTopic mvcTopic)
        //{
        //    return new BllTopic
        //    {
        //        Id = mvcTopic.Id,
        //        Title = mvcTopic.Title,
        //        Text = mvcTopic.Text,
        //        Date = mvcTopic.Date,
        //        IsAnswered = mvcTopic.IsAnswered,
        //        Section = mvcTopic.Section.ToBllSection(),
        //        Author = mvcTopic.Author.ToBllUser(),
        //        Status = mvcTopic.Status.ToBllStatus()
        //    };
        //}

        //public static MvcTopic ToMvcTopic(this BllTopic bllTopic)
        //{
        //    return new MvcTopic
        //    {
        //        Id = bllTopic.Id,
        //        Title = bllTopic.Title,
        //        Text = bllTopic.Text,
        //        Date = bllTopic.Date,
        //        IsAnswered = bllTopic.IsAnswered,
        //        Section = bllTopic.Section.ToMvcSection(),
        //        Author = bllTopic.Author.ToMvcUser(),
        //        Status = bllTopic.Status.ToMvcStatus()
        //    };
        //}

        public static TopicListModel ToTopicListModel(this BllTopic bllTopic)
        {
            return new TopicListModel
            {
                Id = bllTopic.Id,
                Title = bllTopic.Title,
                AuthorLogin = bllTopic.Author.Login,
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
                Author = bllTopic.Author.ToShortUserModel(),
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
                Author = new BllUser { Id = topic.AuthorId },
                Title = topic.Title,
                Text = topic.Text,
                Section = new BllSection() { Name = topic.Section},
            };
        }

        public static CreateEditTopicModel ToCreateEditTopicModel(this BllTopic bllTopic)
        {
            return new CreateEditTopicModel
            {
                Id = bllTopic.Id,
                AuthorId = bllTopic.Author.Id,
                Title = bllTopic.Title,
                Text = bllTopic.Text,
                Section = bllTopic.Section.Name,
            };
        }
    }
}