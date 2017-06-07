using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ForumDal.Interface.Models;

namespace ForumDal.Interface.Repositories
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        IEnumerable<TEntity> GetAll();

        TEntity GetById(int key);

        TEntity GetByPredicate(Expression<Func<TEntity, bool>> f);

        void Create(TEntity e);

        void Delete(TEntity e);

        void Update(TEntity entity);
    }
}
