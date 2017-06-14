using GalaxyForum.Models;
using ForumBll.Interface.Models;

namespace GalaxyForum.Infrastructure.Mappers
{
    public static class MvcModelMapper
    {
        #region User Mapper

        public static BllUser ToBllUser(this MvcUser mvcUser)
        {
            return new BllUser()
            {
                Id = mvcUser.Id,
                Login = mvcUser.Login,
                Email = mvcUser.Email,
                Password = mvcUser.Password,
                Name = mvcUser.Name,
                LastName = mvcUser.LastName,
                FatherName = mvcUser.FatherName,
                Profession = mvcUser.Profession,
                ExtraInfo = mvcUser.ExtraInfo,
                Role = mvcUser.Role.ToBllRole()
            };
        }

        public static BllUser ToBllUser(this RegisterModel mvcUser)
        {
            return new BllUser()
            {
                Login = mvcUser.Login,
                Email = mvcUser.Email,
                Password = mvcUser.Password
            };
        }

        public static MvcUser ToMvcUser(this BllUser bllUser)
        {
            return new MvcUser()
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
                Role = bllUser.Role.ToMvcRole()
            };
        }

        #endregion
        
        #region Topic Mapper

        public static BllTopic ToBllTopic(this MvcTopic mvcTopic)
        {
            return new BllTopic
            {
                Id = mvcTopic.Id,
                Title = mvcTopic.Title,
                Text = mvcTopic.Text,
                Date = mvcTopic.Date,
                IsAnswered = mvcTopic.IsAnswered,
                Section = mvcTopic.Section.ToBllSection(),
                Author = mvcTopic.Author.ToBllUser(),
                Status = mvcTopic.Status.ToBllStatus()
            };
        }

        public static MvcTopic ToMvcTopic(this BllTopic bllTopic)
        {
            return new MvcTopic
            {
                Id = bllTopic.Id,
                Title = bllTopic.Title,
                Text = bllTopic.Text,
                Date = bllTopic.Date,
                IsAnswered = bllTopic.IsAnswered,
                Section = bllTopic.Section.ToMvcSection(),
                Author = bllTopic.Author.ToMvcUser(),
                Status = bllTopic.Status.ToMvcStatus()
            };
        }

        #endregion

        #region Comment Mapper

        public static BllComment ToBllComment(this MvcComment mvcComment)
        {
            return new BllComment
            {
                Id = mvcComment.Id,
                Text = mvcComment.Text,
                Date = mvcComment.Date,
                IsAnswer = mvcComment.IsAnswer,
                Topic = mvcComment.Topic.ToBllTopic(),
                Sender = mvcComment.Sender.ToBllUser(),
                Status = mvcComment.Status.ToBllStatus()
            };
        }

        public static MvcComment ToMvcComment(this BllComment bllComment)
        {
            return new MvcComment
            {
                Id = bllComment.Id,
                Text = bllComment.Text,
                Date = bllComment.Date,
                IsAnswer = bllComment.IsAnswer,
                Topic = bllComment.Topic.ToMvcTopic(),
                Sender = bllComment.Sender.ToMvcUser(),
                Status = bllComment.Status.ToMvcStatus()
            };
        }

        #endregion
    
        #region Role Mapper

        public static BllRole ToBllRole(this MvcRole mvcRole)
        {
            return new BllRole()
            {
                Id = mvcRole.Id,
                Name = mvcRole.Name
            };
        }

        public static MvcRole ToMvcRole(this BllRole bllRole)
        {
            return new MvcRole()
            {
                Id = bllRole.Id,
                Name = bllRole.Name
            };
        }

        #endregion
        
        #region Status Mapper

        public static BllStatus ToBllStatus(this MvcStatus mvcStatus)
        {
            return new BllStatus()
            {
                Id = mvcStatus.Id,
                Name = mvcStatus.Name
            };
        }

        public static MvcStatus ToMvcStatus(this BllStatus bllStatus)
        {
            return new MvcStatus()
            {
                Id = bllStatus.Id,
                Name = bllStatus.Name
            };
        }

        #endregion

        #region Section Mapper

        public static BllSection ToBllSection(this MvcSection mvcSection)
        {
            return new BllSection()
            {
                Id = mvcSection.Id,
                Name = mvcSection.Name
            };
        }

        public static MvcSection ToMvcSection(this BllSection bllSection)
        {
            return new MvcSection()
            {
                Id = bllSection.Id,
                Name = bllSection.Name
            };
        }

        #endregion
    
    }
}