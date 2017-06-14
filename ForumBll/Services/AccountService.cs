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
            newUser.Role = new BllRole() { Id = 3, Name = "User" };

            userRepository.Add(newUser.ToDalUser());
            return userRepository.IsExists(u => (u.Login == newUser.Login));
        }

        public bool IsLoginOrEmailExist(string login, string email)
        {
            return userRepository.IsExists(u => (u.Login == login) || (u.Email == email));
        }

        public BllUser Login(string email, string password)
        {
            DalUser user = userRepository
                .FirstOrDefault(u => (u.Email == email) && (u.Password == password));
            return (user == null) ? null : user.ToBllUser();
        }
    }
}
