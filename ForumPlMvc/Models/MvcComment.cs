using System;

namespace ForumPlMvc.Models
{
    public class MvcComment
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public DateTime Date { get; set; }

        public bool IsAnswer { get; set; }

        public MvcTopic Topic { get; set; }

        public MvcUser Sender { get; set; }

        public MvcStatus Status { get; set; }
    }
}
