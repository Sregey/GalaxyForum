using System;
using System.Collections.Generic;

namespace ForumBll.Interface.Models
{
    public class BllSection
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<BllTopic> Topics { get; set; }
    }
}
