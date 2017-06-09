using ForumDal.Interface.Models;
using ForumOrm.Models;

namespace ForumDal.Mappers
{
    public static class DalMapper
    {
        private static OrmToDalMapperVisitor toDalMapper = new OrmToDalMapperVisitor();
        private static DalToOrmMapperVisitor toOrmMapper = new DalToOrmMapperVisitor();

        public static DalEntity ToDalEntity(this Entity e)
        {
            e.Accept(toDalMapper);
            return (DalEntity)toDalMapper.DalEntity;
        }

        public static Entity ToOrmEntity(this DalEntity e)
        {
            e.Accept(toOrmMapper);
            return (Entity)toOrmMapper.OrmEntity;
        }

        public static DalUser toDal(this User user)
        {
            return new DalUser()
            {
                Id = user.Id,
                Login = user.Login,
                Email = user.Email,
                Password = user.Password,
                Name = user.Name,
                LastName = user.LastName,
                FatherName = user.FatherName,
                Profession = user.Profession,
                ExtraInfo = user.ExtraInfo,
                Role = new DalRole { Id = user.Role.Id, Name = user.Role.Name }
            };
        }
    }
}
