using System;
using System.Collections.Generic;
using System.Linq;
using ForumBll.Interface.Models;
using ForumBll.Interface.Services;
using ForumBll.Mappers;
using ForumDal.Interface.Models;
using ForumDal.Interface.Repositories;

namespace ForumBll.Services
{
    public class UserService : IUserService
    {
        //private readonly IUnitOfWork uow;
        private readonly IRepository<DalUser> userRepository;

        public UserService(/*IUnitOfWork uow, */IRepository<DalUser> repository)
        {
            //this.uow = uow;
            this.userRepository = repository;
        }

        public BllUser GetUser(int id)
        {
            return userRepository
                .FirstOrDefault(u => u.Id == id)
                .ToBllUser();
        }

        public void DeleteUser(int id)
        {
            userRepository.Delete(id);
            //uow.Commit();
        }

        public IEnumerable<BllUser> GetAllUsers()
        {
            /*return userRepository
                .GetByPredicate(dalU => dalU.Id % 2 == 0)
                .Select(user => user.ToBllUser());*/
            return userRepository
                .GetSequence(0, userRepository.Count)
                .Select(user => user.ToBllUser());
        }

        public void AddUser(BllUser user)
        {
            userRepository.Add(user.ToDalUser());
            //uow.Commit();
        }

        public BllUser GetUser(string login)
        {
            return userRepository
                .FirstOrDefault(u => u.Login == login)
                .ToBllUser();
        }

        public void UpdateUser(BllUser user)
        {
            userRepository.Update(user.ToDalUser());
        }

        public void Dispose()
        {
            userRepository.Dispose();
        }
    }
}
