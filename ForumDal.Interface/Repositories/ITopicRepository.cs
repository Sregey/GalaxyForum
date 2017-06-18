using System;
using System.Collections.Generic;
using ForumDal.Interface.Models;
using System.Linq.Expressions;

namespace ForumDal.Interface.Repositories
{
    public interface ITopicRepository : IRepository<DalTopic>
    {
        /// <exception cref = "ArgumentNullException" />
        /// <exception cref = "InvalidOperationException" />
        IEnumerable<DalTopic> Search(Expression<Func<DalTopic, bool>> predicate, string subString);
    }
}
