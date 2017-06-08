using ForumDal.Interface.Models;
using ForumOrm;

namespace ForumDal.Mappers
{
    public static class DalEntityMappers
    {
        #region User Mapper

        public static DalUser ToDalUser(this User user)
        {
            return new DalUser()
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
                Role = user.Role.ToDalRole()
            };
        }

        public static User ToOrmUser(this DalUser dalUser)
        {
            return new User()
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
                Role = dalUser.Role.ToOrmRole()
            };
        }

        #endregion

        #region Topic Mapper

        public static DalTopic ToDalTopic(this Topic topic)
        {
            return new DalTopic
            {
                Id = topic.Id,
                Title = topic.Title,
                Text = topic.Text,
                Date = topic.Date,
                IsAnswered = topic.IsAnswered,
                Section = topic.Section.ToDalSection(),
                Author = topic.Author.ToDalUser(),
                Status = topic.Status.ToDalStatus()
            };
        }

        public static Topic ToOrmTopic(this DalTopic dalTopic)
        {
            return new Topic
            {
                Id = dalTopic.Id,
                Title = dalTopic.Title,
                Text = dalTopic.Text,
                Date = dalTopic.Date,
                IsAnswered = dalTopic.IsAnswered,
                Section = dalTopic.Section.ToOrmSection(),
                Author = dalTopic.Author.ToOrmUser(),
                Status = dalTopic.Status.ToOrmStatus()
            };
        }

        #endregion

        #region Comment Mapper

        public static DalComment ToDalComment(this Comment comment)
        {
            return new DalComment
            {
                Id = comment.Id,
                Text = comment.Text,
                Date = comment.Date,
                IsAnswer = comment.IsAnswer,
                Topic = comment.Topic.ToDalTopic(),
                Sender = comment.Sender.ToDalUser(),
                Status = comment.Status.ToDalStatus()
            };
        }

        public static Comment ToOrmComment(this DalComment dalComment)
        {
            return new Comment
            {
                Id = dalComment.Id,
                Text = dalComment.Text,
                Date = dalComment.Date,
                IsAnswer = dalComment.IsAnswer,
                Topic = dalComment.Topic.ToOrmTopic(),
                Sender = dalComment.Sender.ToOrmUser(),
                Status = dalComment.Status.ToOrmStatus()
            };
        }

        #endregion

        #region Role Mapper

        public static DalRole ToDalRole(this Role role)
        {
            return new DalRole()
            {
                Id = role.Id,
                Name = role.Name
            };
        }

        public static Role ToOrmRole(this DalRole dalRole)
        {
            return new Role()
            {
                Id = dalRole.Id,
                Name = dalRole.Name
            };
        }

        #endregion

        #region Status Mapper

        public static DalStatus ToDalStatus(this Status status)
        {
            return new DalStatus()
            {
                Id = status.Id,
                Name = status.Name
            };
        }

        public static Status ToOrmStatus(this DalStatus dalStatus)
        {
            return new Status()
            {
                Id = dalStatus.Id,
                Name = dalStatus.Name
            };
        }

        #endregion

        #region Section Mapper

        public static DalSection ToDalSection(this Section section)
        {
            return new DalSection()
            {
                Id = section.Id,
                Name = section.Name
            };
        }

        public static Section ToOrmSection(this DalSection dalSection)
        {
            return new Section()
            {
                Id = dalSection.Id,
                Name = dalSection.Name
            };
        }

        #endregion
    }
}
