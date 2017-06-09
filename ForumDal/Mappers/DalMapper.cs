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
    }
}
