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

        #region Public Methods

        public BllUser GetUser(int id)
        {
            return userRepository.GetById(id).ToBllUser();
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
                .GetAll()
                .Select(user => user.ToBllUser());
        }

        public void AddUser(BllUser user)
        {
            userRepository.Add(user.ToDalUser());
            //uow.Commit();
        }
        #endregion
    }
}
