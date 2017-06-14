﻿using System;

namespace ForumDal.Interface.Models
{
    public class DalUser : DalEntity
    {
        public string Login { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string FatherName { get; set; }

        public string Profession { get; set; }

        public string ExtraInfo { get; set; }

        public DateTime RegisrationDate { get; set; }

        public DalImage Avatar { get; set; }

        public DalRole Role { get; set; }
    }
}
