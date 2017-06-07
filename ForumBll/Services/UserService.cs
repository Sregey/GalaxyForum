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
        //public UserEntity GetUserEntity(int id)
        //{
        //    return userRepository.GetById(id).ToBllUser();
        //}        
        //public IEnumerable<UserEntity> GetAllUserEntities()
        //{
        //    return userRepository.GetAll().Select(user => user.ToBllUser());
        //}
        //public void CreateUser(UserEntity user)
        //{
        //    userRepository.Create(user.ToDalUser());
        //    uow.Commit();
        //}
        //public void DeleteUser(UserEntity user)
        //{
        //    userRepository.Delete(user.ToDalUser());
        //    uow.Commit();
        //}

        #region Public Methods

        public BllUser GetUserEntity(int id)
        {
            return userRepository.GetById(id).ToBllUser();
        }

        public void DeleteUser(BllUser user)
        {
            userRepository.Delete(user.ToDalUser());
            //uow.Commit();
        }

        public IEnumerable<BllUser> GetAllUserEntities()
        {
            return userRepository.GetAll().Select(user => user.ToBllUser());
        }

        public void CreateUser(BllUser user)
        {
            userRepository.Create(user.ToDalUser());
            //uow.Commit();
        }
        #endregion
    }
}
