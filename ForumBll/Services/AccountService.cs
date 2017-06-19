using System;
using System.Collections.Generic;
using System.Linq;
using ForumBll.Interface.Models;
using ForumBll.Interface.Services;
using ForumBll.Mappers;
using ForumDal.Interface.Models;
using ForumDal.Interface.Repositories;
using System.Security.Cryptography;
using System.Text;
using ForumBll.Logger;

namespace ForumBll.Services
{
    public class AccountService : IAccountService
    {
        private ILogger logger;

        private readonly IRepository<DalUser> userRepository;

        public AccountService(IRepository<DalUser> repository,
            ILogger logger)
        {
            this.userRepository = repository;
            this.logger = logger;
        }

        public bool RegisterUser(BllUser newUser)
        {
            newUser.RegisrationDate = DateTime.Now;
            newUser.Role = new BllRole() { Id = (int)RoleEnum.User };
            newUser.HashedPassword = Hash(newUser.Password);

            try
            {
                userRepository.Add(newUser.ToDalUser());
                return userRepository.IsExists(u => (u.Login == newUser.Login));
            }
            catch (InvalidOperationException e)
            {
                logger.Warn(e.Message);
                throw;
            }
        }


        public bool IsLoginExist(string login, int excludeUserId)
        {
            bool isExist = IsLoginExist(login);
            DalUser user = null;
            if (isExist)
                user = userRepository.FirstOrDefault(u => u.Id == excludeUserId);
            return isExist && ((user == null) || (user.Login != login));
        }

        public bool IsLoginExist(string login)
        {
            return userRepository.IsExists(u => (u.Login == login));
        }

        public bool IsEmailExist(string email)
        {
            return userRepository.IsExists(u => (u.Email == email));
        }

        public BllUser Login(string email, string password)
        {
            byte[] hashedPassword = Hash(password);
            DalUser user = userRepository
                .FirstOrDefault(u => (u.Email == email) && (u.Password == hashedPassword));
            //return (user == null) ? null : user.ToBllUser();
            return user?.ToBllUser();
        }

        public bool ChangePassword(string login, string oldPassword, string newPassword)
        {
            DalUser user = userRepository.First(u => u.Login == login);
            if (user.Password.SequenceEqual(Hash(oldPassword)))
            {
                user.Password = Hash(newPassword);
                try
                {
                    userRepository.Update(user);
                    return true;
                }
                catch (InvalidOperationException e)
                {
                    logger.Warn(e.Message);
                    throw;
                }
            }
            return false;
        }

        private byte[] Hash(string str)
        {
            byte[] data = Encoding.Unicode.GetBytes(str);
            return new SHA256Managed().ComputeHash(data);
        }

        public void Dispose()
        {
            userRepository.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
