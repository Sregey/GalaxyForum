using ForumPlMvc.Models;
using ForumBll.Interface.Models;
using System.Web;

namespace ForumPlMvc.Infrastructure.Mappers
{
    public static class MvcModelMapper
    {        

        //#region Comment Mapper

        //public static BllComment ToBllComment(this MvcComment mvcComment)
        //{
        //    return new BllComment
        //    {
        //        Id = mvcComment.Id,
        //        Text = mvcComment.Text,
        //        Date = mvcComment.Date,
        //        IsAnswer = mvcComment.IsAnswer,
        //        Topic = mvcComment.Topic.ToBllTopic(),
        //        Sender = mvcComment.Sender.ToBllUser(),
        //        Status = mvcComment.Status.ToBllStatus()
        //    };
        //}

        //public static MvcComment ToMvcComment(this BllComment bllComment)
        //{
        //    return new MvcComment
        //    {
        //        Id = bllComment.Id,
        //        Text = bllComment.Text,
        //        Date = bllComment.Date,
        //        IsAnswer = bllComment.IsAnswer,
        //        Topic = bllComment.Topic.ToMvcTopic(),
        //        Sender = bllComment.Sender.ToMvcUser(),
        //        Status = bllComment.Status.ToMvcStatus()
        //    };
        //}

        //#endregion
    
        /*#region Role Mapper

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
        }*/

        public static SectionModel ToSectionModel(this BllSection bllSection)
        {
            return new SectionModel()
            {
                Id = bllSection.Id,
                Name = bllSection.Name
            };
        }

        //#endregion

        #region Image Mapper

        public static BllImage ToBllImage(this HttpPostedFileBase image)
        {
            string imageType = image.ContentType.Substring(6); //delete "image/" string from type
            return new BllImage()
            {
                Size = image.ContentLength,
                Content = image.InputStream,
                Type = imageType,
            };
        }

        #endregion
    }
}