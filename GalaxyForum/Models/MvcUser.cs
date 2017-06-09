using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GalaxyForum.Models
{
    public class MvcUser
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

        public DateTime RegisrationDate { get; set; }

        public MvcRole Role { get; set; }
    }
}