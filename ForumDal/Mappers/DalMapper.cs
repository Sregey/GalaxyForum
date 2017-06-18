using System;
using ForumDal.Interface.Models;
using ForumOrm.Models;

namespace ForumDal.Mappers
{
    static class DalMapper
    {
        private static OrmToDalMapperVisitor toDalMapper = new OrmToDalMapperVisitor();
        private static DalToOrmMapperVisitor toOrmMapper = new DalToOrmMapperVisitor();

        public static DalEntity ToDalEntity(this Entity e)
        {
            if (e == null)
                throw new ArgumentNullException();

            e.Accept(toDalMapper);
            return (DalEntity)toDalMapper.DalEntity;
        }

        public static Entity ToOrmEntity(this DalEntity e)
        {
            if (e == null)
                throw new ArgumentNullException();

            e.Accept(toOrmMapper);
            return (Entity)toOrmMapper.OrmEntity;
        }
    }
}
