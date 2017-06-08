using System.Collections.Generic;

namespace ForumOrm
{
    public class Section
    {
        public Section()
        {
            Topics = new List<Topic>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Topic> Topics { get; set; }
    }
}
