using ForumDal.Interface.Models;
using ForumOrm.Models;
using System.IO;

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
                TopicId = dalComment.Topic.Id,
                SenderId = dalComment.Sender?.Id,
                StatusId = dalComment.Status.Id,
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

        public void Visit(DalImage dalImage)
        {
            var content = new byte[dalImage.Size];
            dalImage.Content.Seek(0, SeekOrigin.Begin);
            dalImage.Content.Read(content, 0, dalImage.Size);
            OrmEntity = new Image()
            {
                Id = dalImage.Id,
                Name = dalImage.Name,
                Type = dalImage.Type,
                Size = dalImage.Size,
                Content = content,
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
                SectionId = dalTopic.Section.Id,
                AuthorId = dalTopic.Author?.Id,
                StatusId = dalTopic.Status.Id,
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
                RegisrationDate = dalUser.RegisrationDate,
                Profession = dalUser.Profession,
                ExtraInfo = dalUser.ExtraInfo,
                RoleId = dalUser.Role.Id,
            };
            if (dalUser.Avatar != null)
                ((User)OrmEntity).AvatarId = dalUser.Avatar.Id;
        }
    }
}
