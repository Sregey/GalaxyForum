using System.Linq;
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
                HashedPassword = dalUser.Password,
                Name = dalUser.Name,
                LastName = dalUser.LastName,
                FatherName = dalUser.FatherName,
                RegisrationDate = dalUser.RegisrationDate,
                Profession = dalUser.Profession,
                Avatar = dalUser.Avatar.ToBllImage(),
                ExtraInfo = dalUser.ExtraInfo,
                Role = dalUser.Role.ToBllRole(),
                Topics = dalUser.Topics.Select(t => t.ToBllTopic()),
            };
        }

        public static DalUser ToDalUser(this BllUser bllUser)
        {
            return new DalUser()
            {
                Id = bllUser.Id,
                Login = bllUser.Login,
                Email = bllUser.Email,
                Password = bllUser.HashedPassword,
                Name = bllUser.Name,
                LastName = bllUser.LastName,
                FatherName = bllUser.FatherName,
                RegisrationDate = bllUser.RegisrationDate,
                Profession = bllUser.Profession,
                Avatar = bllUser.Avatar.ToDalImage(),
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
                Status = dalTopic.Status.ToBllStatus(),
                Comments = dalTopic.Comments.Select(c => c.ToBllComment()),
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
                Author = new DalUser { Id = bllTopic.Author.Id},
                Status = new DalStatus { Id = bllTopic.Status.Id},
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
                Topic = new DalTopic() { Id = bllComment.Topic.Id},
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
                Name = dalSection.Name,
                Topics = dalSection.Topics.Select(t => t.ToBllTopic()),
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

        #region Image Mapper

        public static BllImage ToBllImage(this DalImage dalImage)
        {
            return new BllImage()
            {
                Id = dalImage.Id,
                Name = dalImage.Name,
                Type = dalImage.Type,
                Size = dalImage.Size,
                Content = dalImage.Content,
            };
        }

        public static DalImage ToDalImage(this BllImage bllImage)
        {
            return new DalImage()
            {
                Id = bllImage.Id,
                Name = bllImage.Name,
                Type = bllImage.Type,
                Size = bllImage.Size,
                Content = bllImage.Content,
            };
        }

        #endregion
    }
}
