using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ForumDal.Interface.Models;

namespace ForumDal.Interface.Repositories
{
    public interface IRepository<TEntity> : IDisposable where TEntity : DalEntity
    {
        int Count { get; }

        /// <exception cref="ArgumentException" />
        IEnumerable<TEntity> GetSequence(int offset, int count);

        /// <exception cref="ArgumentNullException" />
        /// <exception cref="InvalidOperationException" />
        TEntity First(Expression<Func<TEntity, bool>> predicate);

        /// <exception cref="ArgumentNullException" />
        /// <exception cref="InvalidOperationException" />
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);

        /// <exception cref="ArgumentNullException" />
        /// <exception cref="ArgumentException" />
        /// <exception cref="InvalidOperationException" />
        IEnumerable<TEntity> GetByPredicate(Expression<Func<TEntity, bool>> predicate, int offset, int count);

        /// <exception cref="ArgumentNullException" />
        /// <exception cref="InvalidOperationException" />
        IEnumerable<TEntity> GetByPredicate(Expression<Func<TEntity, bool>> predicate);

        /// <exception cref="ArgumentNullException" />
        /// <exception cref="InvalidOperationException" />
        bool IsExists(Expression<Func<TEntity, bool>> predicate);

        /// <exception cref="ArgumentNullException" />
        /// <exception cref="InvalidOperationException" />
        void Add(TEntity entity);

        /// <exception cref="ArgumentException" />
        /// <exception cref="InvalidOperationException" />
        void Delete(int id);

        /// <exception cref="ArgumentNullException" />
        /// <exception cref="InvalidOperationException" />
        void Update(TEntity entity);
    }
}
