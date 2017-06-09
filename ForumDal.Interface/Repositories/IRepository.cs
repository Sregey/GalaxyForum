using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ForumDal.Interface.Models;

namespace ForumDal.Interface.Repositories
{
    public interface IRepository<TEntity> where TEntity : DalEntity
    {
        IEnumerable<TEntity> GetAll();

        TEntity GetById(int id);

        IEnumerable<TEntity> GetByPredicate(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);

        void Delete(int id);

        void Update(TEntity entity);
    }
}
