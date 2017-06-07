using ForumBll.Interface.Models;
using ForumDal.Interface.Models;

namespace ForumBll.Mappers
{
    public static class BllEntityMappers
    {
        public static DalUser ToDalUser(this BllUser userEntity)
        {
            return new DalUser()
            {
                Id = userEntity.Id,
                Name = userEntity.UserName,
                RoleId = userEntity.RoleId
            };
        }

        public static BllUser ToBllUser(this DalUser dalUser)
        {
            return new BllUser()
            {
                Id = dalUser.Id,
                UserName = dalUser.Name,
                RoleId = dalUser.RoleId
            };
        }
    }
}
