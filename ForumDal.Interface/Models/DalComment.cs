using System;

namespace ForumDal.Interface.Models
{
    public class DalComment : IEntity
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public DateTime Date { get; set; }

        public bool IsAnswer { get; set; }

        public DalTopic Topic { get; set; }

        public DalUser Sender { get; set; }

        public DalStatus Status { get; set; }
    }
}
