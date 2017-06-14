using System.Collections.Generic;
using ForumBll.Interface.Models;

namespace ForumBll.Interface.Services
{
    public interface IUserService
    {
        BllUser GetUser(int id);

        BllUser GetUser(string login);

        IEnumerable<BllUser> GetAllUsers();

        void AddUser(BllUser user);

        void DeleteUser(int id);

        void UpdateUser(BllUser user);
    }
}
