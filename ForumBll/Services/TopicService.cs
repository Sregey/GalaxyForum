using System;
using System.Collections.Generic;
using System.Linq;
using ForumBll.Interface.Models;
using ForumBll.Interface.Services;
using ForumBll.Mappers;
using ForumDal.Interface.Models;
using ForumDal.Interface.Repositories;

namespace ForumBll.Services
{
    public class TopicService : ITopicService
    {
        private readonly IRepository<DalTopic> topicRepository;

        public TopicService(IRepository<DalTopic> repository)
        {
            this.topicRepository = repository;
        }

        public void AddTopic(BllTopic topic)
        {
            topic.Date = DateTime.Now;
            throw new NotImplementedException();
        }

        public void DeleteTopic(int id)
        {
            throw new NotImplementedException();
        }

        public BllTopic GetTopic(int id)
        {
            return topicRepository.FirstOrDefault(t => t.Id == id).ToBllTopic();
        }

        public IEnumerable<BllTopic> GetTopicsFromSection(int sectionId, int offset, int count)
        {
            return topicRepository
                .GetByPredicate(topic => topic.Section.Id == sectionId, offset, count)
                .Select(t => t.ToBllTopic());
        }

        public int GetTopicCountInSection(int sectionId)
        {
            return topicRepository
                .GetByPredicate(topic => topic.Section.Id == sectionId, 0, topicRepository.Count)
                .Count();
        }
    }
}
