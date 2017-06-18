using System.Linq;
using ForumDal.Interface.Models;
using ForumOrm.Models;
using System.IO;

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

        public void Visit(Image image)
        {
            var content = new MemoryStream(image.Content);
            DalEntity = new DalImage()
            {
                Id = image.Id,
                Name = image.Name,
                Type = image.Type,
                Size = image.Size,
                Content = content,
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
                Name = section.Name,
                Topics = section.Topics.Select(t => (DalTopic)t.ToDalEntity()),
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
                Author = (DalUser)topic.Author?.ToDalEntity(),
                Status = (DalStatus)topic.Status.ToDalEntity(),
                IsAnswered = topic.IsAnswered,
                Comments = topic.Comments.Select(c => (DalComment)c.ToDalEntity()),
            };
        }

        public void Visit(User user)
        {
            DalEntity = new DalUser()
            {
                Id = user.Id,
                Login = user.Login,
                Email = user.Email,
                Password = user.Password,
                Name = user.Name,
                LastName = user.LastName,
                FatherName = user.FatherName,
                RegisrationDate = user.RegisrationDate,
                Avatar = (DalImage)user.Avatar.ToDalEntity(),
                Profession = user.Profession,
                ExtraInfo = user.ExtraInfo,
                Role = (DalRole)user.Role.ToDalEntity(),
                Topics = user.Topics.Select(t => (DalTopic)t.ToDalEntity())
            };
        }
    }
}
