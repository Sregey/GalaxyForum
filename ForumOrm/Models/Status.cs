using System.Collections.Generic;

namespace ForumOrm.Models
{
    public class Status : Entity
    {
        public Status()
        {
            Topics = new List<Topic>();
        }

        //public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Topic> Topics { get; set; }
    }
}
