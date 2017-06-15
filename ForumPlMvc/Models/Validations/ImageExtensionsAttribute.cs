using System;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ForumPlMvc.Models.Validations
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class ImageExtensionsAttribute : ValidationAttribute
    {
        private string[] allowedExtensions;

        public ImageExtensionsAttribute(string fileExtensions)
        {
            allowedExtensions = fileExtensions.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
         }

        public override bool IsValid(object value)
        {
            HttpPostedFileBase file = value as HttpPostedFileBase;

            if (file != null)
            {
                var fileName = file.FileName;
                return allowedExtensions.Any(extension => fileName.EndsWith(extension));
            }

            return true;
        }
    }
}
