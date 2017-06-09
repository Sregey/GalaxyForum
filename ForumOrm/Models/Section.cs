using System.Collections.Generic;

namespace ForumOrm.Models
{
    public class Section : Entity
    {
        public Section()
        {
            Topics = new List<Topic>();
        }

        //public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Topic> Topics { get; set; }
    }
}
