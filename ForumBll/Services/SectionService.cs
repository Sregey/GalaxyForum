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
    public class SectionService : ISectionService
    {
        private ILogger logger;

        private readonly IRepository<DalSection> sectionRepository;
        private readonly ITopicRepository topicRepository;

        public SectionService(IRepository<DalSection> sectionRepository,
            ITopicRepository topicRepository,
            ILogger logger)
        {
            this.sectionRepository = sectionRepository;
            this.topicRepository = topicRepository;
            this.logger = logger;
        }

        public void AddSection(BllSection section)
        {
            try
            {
                sectionRepository.Add(section.ToDalSection());
            }
            catch (InvalidOperationException e)
            {
                logger.Warn(e.Message);
                throw;
            }
        }

        public void UpdateSection(BllSection section)
        {
            try
            {
                sectionRepository.Update(section.ToDalSection());
            }
            catch (InvalidOperationException e)
            {
                logger.Warn(e.Message);
                throw;
            }
        }

        public void DeleteSection(int id)
        {
            DalSection section = sectionRepository.First(s => s.Id == id);
            foreach (var t in section.Topics.ToList())
            {
                t.Section.Id = 1;
                topicRepository.Update(t);
            }

            try
            {
                sectionRepository.Delete(id);
            }
            catch (InvalidOperationException e)
            {
                logger.Warn(e.Message);
                throw;
            }
        }

        public IEnumerable<BllSection> GetAllSections()
        {
            return sectionRepository
                .GetSequence(0, sectionRepository.Count)
                .Select(section => section.ToBllSection());
        }

        public BllSection GetSection(int id)
        {
            try
            {
                return sectionRepository
                    .First(s => s.Id == id)
                    .ToBllSection();
            }
            catch (InvalidOperationException e)
            {
                logger.Warn(e.Message);
                throw;
            }
        }

        public void Dispose()
        {
            sectionRepository.Dispose();
        }
    }
}
