using ForumDal.Interface.Models;
using ForumDal.Interface.Repositories;
using ForumDal.Mappers;
using ForumOrm.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace ForumDal.Repositories
{
    public class TopicRepository : Repository<DalTopic, Topic>, ITopicRepository
    {
        public TopicRepository(DbContext context) : base(context)
        { }

        public IEnumerable<DalTopic> Search(Expression<Func<DalTopic, bool>> predicate, string subString)
        {
            if ((predicate == null) || (subString == null))
                throw new ArgumentNullException();

            return GetBy(predicate)
                .Where(t => t.Text.Contains(subString) || t.Title.Contains(subString))
                .AsEnumerable()
                .Select(t => (DalTopic)t.ToDalEntity());
        }
    }
}
