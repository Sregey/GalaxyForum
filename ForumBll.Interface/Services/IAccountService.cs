using System.Collections.Generic;
using ForumBll.Interface.Models;

namespace ForumBll.Interface.Services
{
    public interface IAccountService
    {
        bool IsLoginOrEmailExist(string login, string email);

        bool RegisterUser(BllUser user);

        BllUser Login(string email, string password);

        bool ChangePassword(string login, string oldPassword, string newPassword);
    }
}
