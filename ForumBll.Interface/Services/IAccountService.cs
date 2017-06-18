using System.Collections.Generic;
using ForumBll.Interface.Models;
using System;

namespace ForumBll.Interface.Services
{
    public interface IAccountService : IDisposable
    {
        bool IsEmailExist(string email);

        bool IsLoginExist(string login);

        bool IsLoginExist(string login, int excludeUserId);

        bool RegisterUser(BllUser user);

        BllUser Login(string email, string password);

        bool ChangePassword(string login, string oldPassword, string newPassword);
    }
}
