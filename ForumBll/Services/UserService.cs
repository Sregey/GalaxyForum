using System;
using System.Collections.Generic;
using System.Linq;
using ForumBll.Interface.Models;
using ForumBll.Interface.Services;
using ForumBll.Mappers;
using ForumDal.Interface.Models;
using ForumDal.Interface.Repositories;
using ForumBll.Logger;

namespace ForumBll.Services
{
    public class UserService : IUserService
    {
        private ILogger logger;

        private readonly IRepository<DalUser> userRepository;

        public UserService(IRepository<DalUser> repository, ILogger logger)
        {
            this.userRepository = repository;
            this.logger = logger;
        }

        public BllUser GetUser(int id)
        {
            try
            {
                return userRepository
                    .First(u => u.Id == id)
                    .ToBllUser();
            }
            catch (InvalidOperationException e)
            {
                logger.Warn(e.Message);
                throw;
            }
        }

        public void DeleteUser(int id)
        {
            try
            {
                userRepository.Delete(id);
            }
            catch (InvalidOperationException e)
            {
                logger.Warn(e.Message);
                throw;
            }
        }

        public IEnumerable<BllUser> GetAllUsers()
        {
            return userRepository
                .GetSequence(0, userRepository.Count)
                .Select(user => user.ToBllUser());
        }

        public BllUser GetUser(string login)
        {
            try
            {
                return userRepository
                    .First(u => u.Login == login)
                    .ToBllUser();
            }
            catch (InvalidOperationException e)
            {
                logger.Warn(e.Message);
                throw;
            }
        }

        public void UpdateUser(BllUser user)
        {
            try
            {
                userRepository.Update(user.ToDalUser());
            }
            catch (InvalidOperationException e)
            {
                logger.Warn(e.Message);
                throw;
            }
        }

        public void Dispose()
        {
            userRepository.Dispose();
        }
    }
}
