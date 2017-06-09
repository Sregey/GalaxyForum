using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using ForumDal.Interface.Models;
using ForumDal.Interface.Repositories;
using ForumDal.Mappers;
using ForumOrm.Models;
using System.Diagnostics;

namespace ForumDal.Repositories
{
    public class Repository<TDalEntity, TOrmEntity> : IRepository<TDalEntity> 
        where TOrmEntity : Entity
        where TDalEntity : DalEntity
    {
        private readonly DbContext context;

        public Repository(DbContext context)
        {
            this.context = context;
        }

        public void Add(TDalEntity entity)
        {
            context.Set<TOrmEntity>().Add((TOrmEntity)entity.ToOrmEntity());
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            TOrmEntity ormEntity = context.Set<TOrmEntity>().Single(e => (e.Id == id));
            context.Set<TOrmEntity>().Remove(ormEntity);
            context.SaveChanges();
        }

        public IEnumerable<TDalEntity> GetAll()
        {
            return context.Set<TOrmEntity>().Select(e => (TDalEntity)e.ToDalEntity());
        }

        public TDalEntity GetById(int id)
        {
            TOrmEntity entity = context.Set<TOrmEntity>().FirstOrDefault(e => (e.Id == id));
            return (TDalEntity)entity.ToDalEntity();
        }

        public IEnumerable<TDalEntity> GetByPredicate(Expression<Func<TDalEntity, bool>> predicate)
        {
            ParameterExpression ormEntityParam = Expression.Parameter(typeof(TOrmEntity), predicate.Parameters[0].Name);

            var parameterTypeModifier = new ParameterTypeModifier(typeof(TDalEntity), ormEntityParam);
            Expression<Func<TOrmEntity, bool>> ormPredicate = 
                (Expression<Func<TOrmEntity, bool>>)Expression.Lambda(parameterTypeModifier.Modify(predicate.Body), ormEntityParam);

            return context.Set<TOrmEntity>().Where(ormPredicate).Select(e => (TDalEntity)e.ToDalEntity());
        }

        public void Update(TDalEntity entity)
        {
            context.Entry((TOrmEntity)entity.ToOrmEntity()).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
