using System;

namespace ForumBll.Interface.Models
{
    public class BllComment
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public DateTime Date { get; set; }

        public bool IsAnswer { get; set; }

        public BllTopic Topic { get; set; }

        public BllUser Sender { get; set; }

        public BllStatus Status { get; set; }
    }
}
