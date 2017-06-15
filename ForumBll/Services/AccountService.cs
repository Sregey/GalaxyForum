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

namespace ForumBll.Services
{
    public class AccountService : IAccountService
    {
        private readonly IRepository<DalUser> userRepository;

        public AccountService(IRepository<DalUser> repository)
        {
            this.userRepository = repository;
        }

        public bool RegisterUser(BllUser newUser)
        {
            newUser.RegisrationDate = DateTime.Now;
            newUser.Role = new BllRole() { Id = (int)RoleEnum.User };

            userRepository.Add(newUser.ToDalUser());
            return userRepository.IsExists(u => (u.Login == newUser.Login));
        }

        public bool IsLoginOrEmailExist(string login, string email)
        {
            return userRepository.IsExists(u => (u.Login == login) || (u.Email == email));
        }

        public BllUser Login(string email, string password)
        {
            byte[] hashedPassword = Hash(password);
            DalUser user = userRepository
                .FirstOrDefault(u => (u.Email == email) && (u.Password == hashedPassword));
            return (user == null) ? null : user.ToBllUser();
        }

        public bool ChangePassword(string login, string oldPassword, string newPassword)
        {
            DalUser user = userRepository
                .FirstOrDefault(u => u.Login == login);
            if (user.Password.SequenceEqual(Hash(oldPassword)))
            {
                user.Password = Hash(newPassword);
                userRepository.Update(user);
                return true;
            }
            return false;
        }

        private byte[] Hash(string str)
        {
            byte[] data = Encoding.Unicode.GetBytes(str);
            return new SHA256Managed().ComputeHash(data);
        }
    }
}
