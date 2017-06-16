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
        private readonly IRepository<DalSection> sectionRepository;

        public TopicService(IRepository<DalTopic> topicRepository,
            IRepository<DalSection> sectionRepository)
        {
            this.topicRepository = topicRepository;
            this.sectionRepository = sectionRepository;
        }

        public void AddTopic(BllTopic topic)
        {
            topic.Date = DateTime.Now;
            topic.Section = sectionRepository
                .FirstOrDefault(s => s.Name == topic.Section.Name)
                .ToBllSection();
            topic.Status = new BllStatus() { Id = (int)StatusEnum.Raw };
            topicRepository.Add(topic.ToDalTopic());
        }

        public void DeleteTopic(int id)
        {
            throw new NotImplementedException();
        }

        public BllTopic GetTopic(int id)
        {
            return topicRepository.FirstOrDefault(t => t.Id == id).ToBllTopic();
        }

        public IEnumerable<BllTopic> GetTopics()
        {
            return topicRepository
                .GetByPredicate(topic => true)
                .Select(t => t.ToBllTopic());
        }

        public int GetTopicCountInSection(int sectionId)
        {
            return topicRepository
                .GetByPredicate(topic => topic.Section.Id == sectionId, 0, topicRepository.Count)
                .Count();
        }

        public IEnumerable<BllSection> GetAllSections()
        {
            return sectionRepository
                .GetByPredicate(s => true)
                .Select(section => section.ToBllSection());
        }

        public void UpdateTopic(BllTopic topic)
        {
            DalTopic dalTopic = topicRepository.FirstOrDefault(t => t.Id == topic.Id);
            dalTopic.Title = topic.Title;
            dalTopic.Text = topic.Text;
            dalTopic.Status.Id = topic.Status.Id;
            dalTopic.Section = sectionRepository.FirstOrDefault(s => s.Name == topic.Section.Name);
            topicRepository.Update(dalTopic);
        }

        public BllTopic GetRawTopic()
        {
            return topicRepository.FirstOrDefault(t => t.Status.Id == (int)StatusEnum.Raw)
                .ToBllTopic();
        }

        public void Dispose()
        {
            topicRepository.Dispose();
            sectionRepository.Dispose();
        }
    }
}
