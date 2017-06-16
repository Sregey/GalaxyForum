using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ForumDal.Interface.Models;

namespace ForumDal.Interface.Repositories
{
    public interface IRepository<TEntity> : IDisposable where TEntity : DalEntity
    {
        int Count { get; }

        IEnumerable<TEntity> GetSequence(int offset, int count);

        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);

        IEnumerable<TEntity> GetByPredicate(Expression<Func<TEntity, bool>> predicate, int offset, int count);

        IEnumerable<TEntity> GetByPredicate(Expression<Func<TEntity, bool>> predicate);

        bool IsExists(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);

        void Delete(int id);

        void Update(TEntity entity);
    }
}
