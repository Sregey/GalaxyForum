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
            TOrmEntity orm = (TOrmEntity)entity.ToOrmEntity();
            context.Set<TOrmEntity>().Add((TOrmEntity)entity.ToOrmEntity());
            context.SaveChanges();
        }

        public virtual void Delete(int id)
        {
            TOrmEntity ormEntity = context.Set<TOrmEntity>().Single(e => (e.Id == id));
            context.Set<TOrmEntity>().Remove(ormEntity);
            context.SaveChanges();
        }

        public virtual IEnumerable<TDalEntity> GetAll()
        {
            return context.Set<TOrmEntity>()
                .AsEnumerable()
                .Select(e => (TDalEntity)e.ToDalEntity());
        }

        public virtual IEnumerable<TDalEntity> GetSequence(int offset, int count)
        {
            return context.Set<TOrmEntity>()
                .OrderBy(e => e.Id)
                .Skip(offset)
                .Take(count)
                .AsEnumerable()
                .Select(e => (TDalEntity)e.ToDalEntity());
        }

        public virtual TDalEntity FirstOrDefault(Expression<Func<TDalEntity, bool>> predicate)
        {
            return (TDalEntity)GetBy(predicate)
                .FirstOrDefault()
                .ToDalEntity();
        }

        public virtual IEnumerable<TDalEntity> GetByPredicate(Expression<Func<TDalEntity, bool>> predicate, int offset, int count)
        {
            return GetBy(predicate)
                .OrderBy(e => e.Id)
                .Skip(offset)
                .Take(count)
                .AsEnumerable()
                .Select(e => (TDalEntity)e.ToDalEntity());
        }

        public virtual void Update(TDalEntity entity)
        {
            TOrmEntity ormEntity = context.Set<TOrmEntity>().FirstOrDefault(e => e.Id == entity.Id);
            //context.Set<TOrmEntity>().Attach(ormEntity);
            //context.Entry(ormEntity).State = EntityState.Modified;
            context.Entry(ormEntity).CurrentValues.SetValues((TOrmEntity)entity.ToOrmEntity());
            context.SaveChanges();
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
            ParameterExpression ormEntityParam = Expression.Parameter(typeof(TOrmEntity), predicate.Parameters[0].Name);

            var parameterTypeModifier = new DalToOrmExpressionModifier(ormEntityParam);
            Expression<Func<TOrmEntity, bool>> ormPredicate =
                (Expression<Func<TOrmEntity, bool>>)Expression.Lambda(parameterTypeModifier.Modify(predicate.Body), ormEntityParam);

            return context.Set<TOrmEntity>()
                .Where(ormPredicate);
        }
    }
}
