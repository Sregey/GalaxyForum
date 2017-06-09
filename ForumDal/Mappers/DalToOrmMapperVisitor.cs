using ForumDal.Interface.Models;
using ForumOrm.Models;

namespace ForumDal.Mappers
{
    class DalToOrmMapperVisitor : IDalEntityVisitor
    {
        public Entity OrmEntity { get; private set; }

        public void Visit(DalComment dalComment)
        {
            OrmEntity = new Comment
            {
                Id = dalComment.Id,
                Text = dalComment.Text,
                Date = dalComment.Date,
                IsAnswer = dalComment.IsAnswer,
                Topic = (Topic)dalComment.Topic.ToOrmEntity(),
                Sender = (User)dalComment.Sender.ToOrmEntity(),
                Status = (Status)dalComment.Status.ToOrmEntity(),
            };
        }

        public void Visit(DalSection dalSection)
        {
            OrmEntity = new Section()
            {
                Id = dalSection.Id,
                Name = dalSection.Name
            };
        }

        public void Visit(DalStatus dalStatus)
        {
            OrmEntity = new Status()
            {
                Id = dalStatus.Id,
                Name = dalStatus.Name
            };
        }

        public void Visit(DalRole dalRole)
        {
            OrmEntity = new Role()
            {
                Id = dalRole.Id,
                Name = dalRole.Name
            };
        }

        public void Visit(DalTopic dalTopic)
        {
            OrmEntity = new Topic
            {
                Id = dalTopic.Id,
                Title = dalTopic.Title,
                Text = dalTopic.Text,
                Date = dalTopic.Date,
                IsAnswered = dalTopic.IsAnswered,
                Section = (Section)dalTopic.Section.ToOrmEntity(),
                Author = (User)dalTopic.Author.ToOrmEntity(),
                Status = (Status)dalTopic.Status.ToOrmEntity(),
            };
        }

        public void Visit(DalUser dalUser)
        {
            OrmEntity = new User()
            {
                Id = dalUser.Id,
                Login = dalUser.Login,
                Email = dalUser.Email,
                Password = dalUser.Password,
                Name = dalUser.Name,
                LastName = dalUser.LastName,
                FatherName = dalUser.FatherName,
                Profession = dalUser.Profession,
                ExtraInfo = dalUser.ExtraInfo,
                Role = (Role)dalUser.Role.ToOrmEntity(),
            };
        }
    }
}
