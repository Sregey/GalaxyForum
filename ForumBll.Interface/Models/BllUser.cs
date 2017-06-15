using System;
using System.Collections.Generic;
using System.IO;

namespace ForumBll.Interface.Models
{
    public class BllUser
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string FatherName { get; set; }

        public string Profession { get; set; }

        public string ExtraInfo { get; set; }

        public BllImage Avatar { get; set; }

        public DateTime RegisrationDate { get; set; }

        public BllRole Role { get; set; }

        public virtual IEnumerable<BllTopic> Topics { get; set; }
    }
}
