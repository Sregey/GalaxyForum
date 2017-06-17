using System.Collections.Generic;
using ForumBll.Interface.Models;
using System;

namespace ForumBll.Interface.Services
{
    public interface ITopicService : IDisposable
    {
        BllTopic GetTopic(int id);

        BllTopic GetRawTopic();

        IEnumerable<BllTopic> SearchInSection(int sectionId, string subString);

        IEnumerable<BllSection> GetAllSections();

        IEnumerable<BllTopic> GetTopics();

        void UpdateTopic(BllTopic topic);

        void AddTopic(BllTopic topic);

        void DeleteTopic(int id);
    }
}
