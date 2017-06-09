using System;

namespace GalaxyForum.Models
{
    public class MvcTopic
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public DateTime Date { get; set; }

        public bool IsAnswered { get; set; }

        public MvcSection Section { get; set; }

        public MvcUser Author { get; set; }

        public MvcStatus Status { get; set; }
    }
}
