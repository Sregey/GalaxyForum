using System;

namespace ForumOrm.Models
{
    public class Topic : Entity
    {
        //public int Id { get; set; }

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
    }
}
