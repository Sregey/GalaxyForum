using System.Collections.Generic;
using ForumBll.Interface.Models;

namespace ForumBll.Interface.Services
{
    public interface IUserService
    {
        BllUser GetUserEntity(int id);

        IEnumerable<BllUser> GetAllUserEntities();

        void CreateUser(BllUser user);

        void DeleteUser(BllUser user);
    }
}
