using System;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ForumPlMvc.Models.Validations
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class FileSizeAttribute : ValidationAttribute
    {
        private int maxSize;
        public FileSizeAttribute(int maxSize)
        {
            this.maxSize = maxSize;
        }

        public override bool IsValid(object value)
        {
            HttpPostedFileBase file = value as HttpPostedFileBase;

            if (file != null)
            {
                return file.ContentLength <= maxSize;
            }

            return true;
        }
    }
}