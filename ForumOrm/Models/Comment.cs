using System;

namespace ForumOrm.Models
{
    public class Comment : Entity
    {
        public string Text { get; set; }

        public DateTime Date { get; set; }

        public int TopicId { get; set; }

        public int SenderId { get; set; }

        public int StatusId { get; set; }

        public bool IsAnswer { get; set; }

        //public bool IsActual { get; set; }

        public virtual Topic Topic { get; set; }

        public virtual User Sender { get; set; }

        public virtual Status Status { get; set; }
    }
}
