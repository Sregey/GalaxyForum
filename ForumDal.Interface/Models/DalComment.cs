using System;

namespace ForumDal.Interface.Models
{
    public class DalComment : DalEntity
    {
        public string Text { get; set; }

        public DateTime Date { get; set; }

        public bool IsAnswer { get; set; }

        public DalTopic Topic { get; set; }

        public DalUser Sender { get; set; }

        public DalStatus Status { get; set; }
    }
}
