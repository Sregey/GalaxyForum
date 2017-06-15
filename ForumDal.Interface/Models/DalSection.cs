using System;
using System.Collections.Generic;

namespace ForumDal.Interface.Models
{
    public class DalSection : DalEntity
    {
        public string Name { get; set; }

        public IEnumerable<DalTopic> Topics { get; set; }
    }
}
