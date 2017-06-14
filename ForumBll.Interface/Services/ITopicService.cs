using System.Collections.Generic;
using ForumBll.Interface.Models;

namespace ForumBll.Interface.Services
{
    public interface ITopicService
    {
        BllTopic GetTopic(int id);

        IEnumerable<BllTopic> GetTopicsFromSection(int sectionId, int offset, int count);

        int GetTopicCountInSection(int sectionId);

        void AddTopic(BllTopic topic);

        void DeleteTopic(int id);
    }
}
