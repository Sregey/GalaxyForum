using System.Collections.Generic;
using ForumBll.Interface.Models;

namespace ForumBll.Interface.Services
{
    public interface IUserService
    {
        BllUser GetUser(int id);

        IEnumerable<BllUser> GetAllUsers();

        void AddUser(BllUser user);

        void DeleteUser(int id);
    }
}
