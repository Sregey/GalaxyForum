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

        public SectionService(IRepository<DalSection> repository)
        {
            this.sectionRepository = repository;
        }

        public int Count
        {
            get { return sectionRepository.Count; }
        }

        public void AddSection(BllSection section)
        {
            throw new NotImplementedException();
        }

        public void DeleteSection(int id)
        {
            throw new NotImplementedException();
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
    }
}
