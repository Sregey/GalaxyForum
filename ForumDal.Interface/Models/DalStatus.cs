using System;

namespace ForumDal.Interface.Models
{
    public class DalStatus : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
