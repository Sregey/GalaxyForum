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
    public class SectionService : ISectionService
    {
        private readonly IRepository<DalSection> sectionRepository;
        private readonly ITopicRepository topicRepository;

        public SectionService(IRepository<DalSection> sectionRepository,
            ITopicRepository topicRepository)
        {
            this.sectionRepository = sectionRepository;
            this.topicRepository = topicRepository;
        }

        public int Count
        {
            get { return sectionRepository.Count; }
        }

        public void AddSection(BllSection section)
        {
            sectionRepository.Add(section.ToDalSection());
        }

        public void UpdateSection(BllSection section)
        {
            sectionRepository.Update(section.ToDalSection());
        }

        public void DeleteSection(int id)
        {
            DalSection section = sectionRepository.FirstOrDefault(s => s.Id == id);
            foreach (var t in section.Topics.ToList())
            {
                t.Section.Id = 1;
                topicRepository.Update(t);
            }
            sectionRepository.Delete(id);
        }

        public IEnumerable<BllSection> GetAllSections()
        {
            return sectionRepository
                .GetSequence(0, sectionRepository.Count)
                .Select(section => section.ToBllSection());
        }

        public BllSection GetSection(int id)
        {
            return sectionRepository
                .FirstOrDefault(s => s.Id == id)
                .ToBllSection();
        }

        public void Dispose()
        {
            sectionRepository.Dispose();
        }
    }
}
