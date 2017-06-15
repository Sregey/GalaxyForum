using System;
using System.Collections.Generic;

namespace ForumDal.Interface.Models
{
    public class DalTopic : DalEntity
    {
        public string Title { get; set; }

        public string Text { get; set; }

        public DateTime Date { get; set; }

        public bool IsAnswered { get; set; }

        public DalSection Section { get; set; }

        public DalUser Author { get; set; }

        public DalStatus Status { get; set; }

        public IEnumerable<DalComment> Comments { get; set; }
    }
}
