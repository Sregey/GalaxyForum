using System.Collections.Generic;

namespace ForumOrm.Models
{
    public class Section : Entity
    {
        public Section()
        {
            Topics = new HashSet<Topic>();
        }

        public string Name { get; set; }

        public ICollection<Topic> Topics { get; set; }
    }
}
