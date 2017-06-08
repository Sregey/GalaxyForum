using System;

namespace ForumBll.Interface.Models
{
    public class BllTopic
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public DateTime Date { get; set; }

        public bool IsAnswered { get; set; }

        public BllSection Section { get; set; }

        public BllUser Author { get; set; }

        public BllStatus Status { get; set; }
    }
}
