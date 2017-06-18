using System;
using System.Collections.Generic;

namespace ForumOrm.Models
{
    public class Topic : Entity
    {
        public Topic()
        {
            Comments = new HashSet<Comment>();
        }

        public string Title { get; set; }

        public string Text { get; set; }

        public DateTime Date { get; set; }

        public int SectionId { get; set; }

        public int? AuthorId { get; set; }

        public int StatusId { get; set; }

        public bool IsAnswered { get; set; }

        //public bool IsActual { get; set; }

        public virtual Section Section { get; set; }

        public virtual User Author { get; set; }

        public virtual Status Status { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
