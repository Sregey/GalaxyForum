using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using ForumDal.Interface.Models;
using ForumDal.Interface.Repositories;
using ForumDal.Mappers;
using ForumOrm;

namespace ForumDal.Repositories
{
    public class UserRepository : IRepository<DalUser>
    {
        private readonly DbContext context;

        public UserRepository(DbContext context)
        {
            this.context = context;
        }

        public IEnumerable<DalUser> GetAll()
        {
            return context.Set<User>().Select(u => u.ToDalUser());
        }

        public DalUser GetById(int id)
        {
            User user = context.Set<User>().FirstOrDefault(u => (u.Id == id));
            return user.ToDalUser();
        }

        public DalUser GetByPredicate(Expression<Func<DalUser, bool>> f)
        {
            //Expression<Func<DalUser, bool>> -> Expression<Func<User, bool>> (!)
            throw new NotImplementedException();
        }

        public void Add(DalUser dalUser)
        {
            context.Set<User>().Add(dalUser.ToOrmUser());
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            User user = context.Set<User>().Single(u => (u.Id == id));
            context.Set<User>().Remove(user);
            context.SaveChanges();
        }

        public void Update(DalUser dalUser)
        {
            context.Entry(dalUser.ToOrmUser()).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
