using System;
using System.Collections.Generic;

namespace ForumOrm.Models
{ 
    public class User : Entity
    {
        public User()
        {
            Topics = new List<Topic>();
        }

        //public int Id { get; set; }

        public string Login { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string FatherName { get; set; }

        public string Profession { get; set; }

        public string ExtraInfo { get; set; }

        public DateTime RegisrationDate { get; set; }

        public int RoleId { get; set; }

        public virtual Role Role { get; set; }

        public virtual ICollection<Topic> Topics { get; set; }
    }
}
