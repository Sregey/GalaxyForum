using System.Collections.Generic;
using ForumBll.Interface.Models;
using System;

namespace ForumBll.Interface.Services
{
    public interface IAccountService : IDisposable
    {
        bool IsLoginOrEmailExist(string login, string email);

        bool RegisterUser(BllUser user);

        BllUser Login(string email, string password);

        bool ChangePassword(string login, string oldPassword, string newPassword);
    }
}
