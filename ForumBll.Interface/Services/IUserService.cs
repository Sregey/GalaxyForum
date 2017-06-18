using System.Collections.Generic;
using ForumBll.Interface.Models;
using System;

namespace ForumBll.Interface.Services
{
    public interface IUserService : IDisposable
    {
        BllUser GetUser(int id);

        BllUser GetUser(string login);

        IEnumerable<BllUser> GetAllUsers();

        void DeleteUser(int id);

        void UpdateUser(BllUser user);
    }
}
