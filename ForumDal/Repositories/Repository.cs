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
    public class Repository<TDalEntity, TOrmEntity> : IRepository<TDalEntity>
        where TOrmEntity : Entity
        where TDalEntity : DalEntity
    {
        protected readonly DbContext context;

        public Repository(DbContext context)
        {
            this.context = context;
        }

        public virtual int Count
        {
            get { return context.Set<TOrmEntity>().Count(); }
        }

        public virtual void Add(TDalEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException();

            try
            {
                context.Set<TOrmEntity>().Add((TOrmEntity)entity.ToOrmEntity());
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Cann't add entity to repository.", e);
            }
        }

        public virtual void Delete(int id)
        {
            if (id < 1)
                throw new ArgumentException();

            try
            {
                TOrmEntity ormEntity = context.Set<TOrmEntity>().Single(e => (e.Id == id));
                context.Set<TOrmEntity>().Remove(ormEntity);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Cann't delete entity from repository.", e);
            }
        }

        public virtual IEnumerable<TDalEntity> GetSequence(int offset, int count)
        {
            if ((offset < 0) || (count < 0))
                throw new ArgumentException();

            return context.Set<TOrmEntity>()
                .OrderBy(e => e.Id)
                .Skip(offset)
                .Take(count)
                .AsEnumerable()
                .Select(e => (TDalEntity)e.ToDalEntity());
        }

        public virtual TDalEntity First(Expression<Func<TDalEntity, bool>> predicate)
        {
            try
            {
                return (TDalEntity)GetBy(predicate)
                    .First()
                    .ToDalEntity();
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Cann't find entity in repository.", e);
            }
        }

        public virtual TDalEntity FirstOrDefault(Expression<Func<TDalEntity, bool>> predicate)
        {
            return (TDalEntity)GetBy(predicate)
                .FirstOrDefault()
                ?.ToDalEntity();
        }

        public virtual IEnumerable<TDalEntity> GetByPredicate(Expression<Func<TDalEntity, bool>> predicate, int offset, int count)
        {
            if ((offset < 0) || (count < 0))
                throw new ArgumentException();

            return GetBy(predicate)
                .OrderBy(e => e.Id)
                .Skip(offset)
                .Take(count)
                .AsEnumerable()
                .Select(e => (TDalEntity)e.ToDalEntity());
        }

        public virtual void Update(TDalEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException();

            try
            {
                TOrmEntity ormEntity = context.Set<TOrmEntity>().FirstOrDefault(e => e.Id == entity.Id);
                context.Entry(ormEntity).CurrentValues.SetValues((TOrmEntity)entity.ToOrmEntity());
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Cann't update entity in repository.", e);
            }
        }

        public virtual IEnumerable<TDalEntity> GetByPredicate(Expression<Func<TDalEntity, bool>> predicate)
        {
            return GetBy(predicate)
                .AsEnumerable()
                .Select(e => (TDalEntity)e.ToDalEntity());
        }

        public virtual bool IsExists(Expression<Func<TDalEntity, bool>> predicate)
        {
            return GetBy(predicate).FirstOrDefault() != null;
        }

        public void Dispose()
        {
            context.Dispose();
        }

        protected IQueryable<TOrmEntity> GetBy(Expression<Func<TDalEntity, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException();

            ParameterExpression ormEntityParam = Expression.Parameter(typeof(TOrmEntity), predicate.Parameters[0].Name);

            var parameterTypeModifier = new DalToOrmExpressionModifier(ormEntityParam);
            Expression<Func<TOrmEntity, bool>> ormPredicate =
                (Expression<Func<TOrmEntity, bool>>)Expression.Lambda(parameterTypeModifier.Modify(predicate.Body), ormEntityParam);

            return context.Set<TOrmEntity>()
                .Where(ormPredicate);
        }
    }
}
