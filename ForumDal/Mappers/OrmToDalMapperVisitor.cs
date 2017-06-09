using System;
using ForumDal.Interface.Models;
using ForumOrm.Models;

namespace ForumDal.Mappers
{
    public class OrmToDalMapperVisitor : IEntityVisitor
    {
        public DalEntity DalEntity { get; private set; }

        public void Visit(Comment comment)
        {
            DalEntity = new DalComment
            {
                Id = comment.Id,
                Text = comment.Text,
                Date = comment.Date,
                Topic = (DalTopic)comment.Topic.ToDalEntity(),
                Sender = (DalUser)comment.Sender.ToDalEntity(),
                Status = (DalStatus)comment.Status.ToDalEntity(),
                IsAnswer = comment.IsAnswer,
            };
        }

        public void Visit(Role role)
        {
            DalEntity = new DalRole()
            {
                Id = role.Id,
                Name = role.Name
            };
        }

        public void Visit(Status status)
        {
            DalEntity = new DalStatus()
            {
                Id = status.Id,
                Name = status.Name
            };
        }

        public void Visit(Section section)
        {
            DalEntity = new DalSection()
            {
                Id = section.Id,
                Name = section.Name
            };
        }

        public void Visit(Topic topic)
        {
            DalEntity = new DalTopic
            {
                Id = topic.Id,
                Title = topic.Title,
                Text = topic.Text,
                Date = topic.Date,
                Section = (DalSection)topic.Section.ToDalEntity(),
                Author = (DalUser)topic.Author.ToDalEntity(),
                Status = (DalStatus)topic.Status.ToDalEntity(),
                IsAnswered = topic.IsAnswered,
            };
        }

        public void Visit(User user)
        {
            var mapper = new OrmToDalMapperVisitor();
            user.Role.Accept(mapper);
            DalEntity = new DalUser()
            {
                Id = user.Id,
                Login = user.Login,
                Email = user.Email,
                Password = user.Password,
                Name = user.Name,
                LastName = user.LastName,
                FatherName = user.FatherName,
                Profession = user.Profession,
                ExtraInfo = user.ExtraInfo,
                Role = (DalRole)user.Role.ToDalEntity(),
            };
        }
    }
}
