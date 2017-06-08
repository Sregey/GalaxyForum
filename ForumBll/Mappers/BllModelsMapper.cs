using ForumBll.Interface.Models;
using ForumDal.Interface.Models;

namespace ForumBll.Mappers
{
    public static class BllEntityMappers
    {
        #region User Mapper

        public static BllUser ToBllUser(this DalUser dalUser)
        {
            return new BllUser()
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
                Role = dalUser.Role.ToBllRole()
            };
        }

        public static DalUser ToDalUser(this BllUser bllUser)
        {
            return new DalUser()
            {
                Id = bllUser.Id,
                Login = bllUser.Login,
                Email = bllUser.Email,
                Password = bllUser.Password,
                Name = bllUser.Name,
                LastName = bllUser.LastName,
                FatherName = bllUser.FatherName,
                Profession = bllUser.Profession,
                ExtraInfo = bllUser.ExtraInfo,
                Role = bllUser.Role.ToDalRole()
            };
        }

        #endregion

        #region Topic Mapper

        public static BllTopic ToBllTopic(this DalTopic dalTopic)
        {
            return new BllTopic
            {
                Id = dalTopic.Id,
                Title = dalTopic.Title,
                Text = dalTopic.Text,
                Date = dalTopic.Date,
                IsAnswered = dalTopic.IsAnswered,
                Section = dalTopic.Section.ToBllSection(),
                Author = dalTopic.Author.ToBllUser(),
                Status = dalTopic.Status.ToBllStatus()
            };
        }

        public static DalTopic ToDalTopic(this BllTopic bllTopic)
        {
            return new DalTopic
            {
                Id = bllTopic.Id,
                Title = bllTopic.Title,
                Text = bllTopic.Text,
                Date = bllTopic.Date,
                IsAnswered = bllTopic.IsAnswered,
                Section = bllTopic.Section.ToDalSection(),
                Author = bllTopic.Author.ToDalUser(),
                Status = bllTopic.Status.ToDalStatus()
            };
        }

        #endregion

        #region Comment Mapper

        public static BllComment ToBllComment(this DalComment dalComment)
        {
            return new BllComment
            {
                Id = dalComment.Id,
                Text = dalComment.Text,
                Date = dalComment.Date,
                IsAnswer = dalComment.IsAnswer,
                Topic = dalComment.Topic.ToBllTopic(),
                Sender = dalComment.Sender.ToBllUser(),
                Status = dalComment.Status.ToBllStatus()
            };
        }

        public static DalComment ToDalComment(this BllComment bllComment)
        {
            return new DalComment
            {
                Id = bllComment.Id,
                Text = bllComment.Text,
                Date = bllComment.Date,
                IsAnswer = bllComment.IsAnswer,
                Topic = bllComment.Topic.ToDalTopic(),
                Sender = bllComment.Sender.ToDalUser(),
                Status = bllComment.Status.ToDalStatus()
            };
        }

        #endregion

        #region Role Mapper

        public static BllRole ToBllRole(this DalRole dalRole)
        {
            return new BllRole()
            {
                Id = dalRole.Id,
                Name = dalRole.Name
            };
        }

        public static DalRole ToDalRole(this BllRole bllRole)
        {
            return new DalRole()
            {
                Id = bllRole.Id,
                Name = bllRole.Name
            };
        }

        #endregion

        #region Status Mapper

        public static BllStatus ToBllStatus(this DalStatus dalStatus)
        {
            return new BllStatus()
            {
                Id = dalStatus.Id,
                Name = dalStatus.Name
            };
        }

        public static DalStatus ToDalStatus(this BllStatus bllStatus)
        {
            return new DalStatus()
            {
                Id = bllStatus.Id,
                Name = bllStatus.Name
            };
        }

        #endregion

        #region Section Mapper

        public static BllSection ToBllSection(this DalSection dalSection)
        {
            return new BllSection()
            {
                Id = dalSection.Id,
                Name = dalSection.Name
            };
        }

        public static DalSection ToDalSection(this BllSection bllSection)
        {
            return new DalSection()
            {
                Id = bllSection.Id,
                Name = bllSection.Name
            };
        }

        #endregion
    }
}
