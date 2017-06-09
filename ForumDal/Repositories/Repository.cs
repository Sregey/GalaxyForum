using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using ForumDal.Interface.Models;
using ForumDal.Interface.Repositories;
using ForumDal.Mappers;
using ForumOrm.Models;

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

        public TDalEntity GetByPredicate(Expression<Func<TDalEntity, bool>> f)
        {
            throw new NotImplementedException();
        }

        public void Update(TDalEntity entity)
        {
            DalEntity e = entity;
            e.ToOrmEntity();
            context.Entry((TOrmEntity)entity.ToOrmEntity()).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
