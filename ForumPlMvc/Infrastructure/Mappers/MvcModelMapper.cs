using ForumPlMvc.Models;
using ForumBll.Interface.Models;
using System.Web;

namespace ForumPlMvc.Infrastructure.Mappers
{
    public static class MvcModelMapper
    {
        #region Section Mapper

        public static SectionModel ToSectionModel(this BllSection bllSection)
        {
            return new SectionModel()
            {
                Id = bllSection.Id,
                Name = bllSection.Name
            };
        }

        public static BllSection ToBllSection(this SectionModel section)
        {
            return new BllSection()
            {
                Id = section.Id,
                Name = section.Name
            };
        }

        #endregion

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