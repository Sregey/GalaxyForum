using System;
using System.Collections.Generic;
using System.Linq;
using ForumBll.Interface.Models;
using ForumBll.Interface.Services;
using ForumBll.Mappers;
using ForumDal.Interface.Models;
using ForumDal.Interface.Repositories;
using ForumBll.Logger;

namespace ForumBll.Services
{
    public class TopicService : ITopicService
    {
        private ILogger logger;

        private readonly ITopicRepository topicRepository;
        private readonly IRepository<DalSection> sectionRepository;

        public TopicService(ITopicRepository topicRepository,
            IRepository<DalSection> sectionRepository,
            ILogger logger)
        {
            this.topicRepository = topicRepository;
            this.sectionRepository = sectionRepository;
            this.logger = logger;
        }

        public void AddTopic(BllTopic topic)
        {
            topic.Date = DateTime.Now;
            topic.Status = new BllStatus() { Id = (int)StatusEnum.Raw };
            try
            {
                topicRepository.Add(topic.ToDalTopic());
            }
            catch (InvalidOperationException e)
            {
                logger.Warn(e.Message);
                throw;
            }
        }

        public void DeleteTopic(int id)
        {
            try
            {
                topicRepository.Delete(id);
            }
            catch (InvalidOperationException e)
            {
                logger.Warn(e.Message);
                throw;
            }
        }

        public BllTopic GetTopic(int id)
        {
            try
            {
                return topicRepository.First(t => t.Id == id).ToBllTopic();
            }
            catch (InvalidOperationException e)
            {
                logger.Warn(e.Message);
                throw;
            }
        }

        public IEnumerable<BllTopic> GetTopics()
        {
            return topicRepository
                .GetByPredicate(topic => true)
                .Select(t => t.ToBllTopic());
        }

        public IEnumerable<BllSection> GetAllSections()
        {
            return sectionRepository
                .GetByPredicate(s => true)
                .Select(section => section.ToBllSection());
        }

        public void UpdateTopic(BllTopic topic)
        {
            DalTopic dalTopic = topicRepository.First(t => t.Id == topic.Id);
            dalTopic.Title = topic.Title;
            dalTopic.Text = topic.Text;
            dalTopic.Status.Id = topic.Status.Id;
            dalTopic.Section.Id = topic.Section.Id;
            try
            {
                topicRepository.Update(dalTopic);
            }
            catch (InvalidOperationException e)
            {
                logger.Warn(e.Message);
                throw;
            }
        }

        public BllTopic GetRawTopic()
        {
            return topicRepository.FirstOrDefault(t => t.Status.Id == (int)StatusEnum.Raw)?
                .ToBllTopic();
        }

        public IEnumerable<BllTopic> SearchInSection(int sectionId, string subString)
        {
            return topicRepository.Search(t => t.Section.Id == sectionId, subString)
                .Select(t => t.ToBllTopic());
        }

        public void Dispose()
        {
            topicRepository.Dispose();
            sectionRepository.Dispose();
        }
    }
}
