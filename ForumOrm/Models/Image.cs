﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumOrm.Models
{
    public class Image : Entity
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public int Size { get; set; }

        public byte[] Content { get; set; }
    }
}
