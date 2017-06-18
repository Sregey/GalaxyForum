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
    public class UserRepository : Repository<DalUser, User>
    {
        public UserRepository(DbContext context) : base(context)
        { }

        public override void Update(DalUser dalUser)
        {
            User user = context.Set<User>()
                .Include(u => u.Avatar)
                .FirstOrDefault(e => e.Id == dalUser.Id);

            User us = (User)dalUser.ToOrmEntity();
            context.Entry(user).CurrentValues.SetValues(us);
            user.Avatar = (Image)dalUser.Avatar.ToOrmEntity();

            context.SaveChanges();
        }

        public override void Delete(int id)
        {
            context.Set<Topic>().Where(t => t.AuthorId == id).Load();
            context.Set<Comment>().Where(c => c.SenderId == id).Load();
            base.Delete(id);
        }
    }
}
