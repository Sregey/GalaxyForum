using System.Collections.Generic;

namespace ForumOrm.Models
{
    public class Role : Entity
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        //public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
