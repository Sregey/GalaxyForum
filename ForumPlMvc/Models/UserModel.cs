using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Web;
using ForumPlMvc.Models.Validations;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ForumPlMvc.Models
{
    public class UserInfoModel : UserSettingModel
    {
        public string Login { get; set; }

        public string Email { get; set; }

        public int AvatarId { get; set; }
    }

    public class UserSettingModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Display(Name = "Father name")]
        public string FatherName { get; set; }

        public string Profession { get; set; }

        [Display(Name = "Extra information")]
        public string ExtraInfo { get; set; }

        [ImageExtensions("jpg,jpeg,png", ErrorMessage = "Only jpg, jpeg, png extensions are available.")]
        [FileSize(3 * 1024 * 1024, ErrorMessage = "Max size is 3 Mb.")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase Avatar { get; set; }
    }

    public class UserAdminEditModel : UserSettingModel
    {
        [Required(ErrorMessage = "Login is required.")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Login length must be between 5 to 20 characters.")]
        [Remote("CheckLogin", "Account", AdditionalFields = "Id", ErrorMessage = "Login already exists.")]
        [RegularExpression(@"\w*",
            ErrorMessage = "Loging must consists of Latin letters, digits and underscores. ")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        public string Role { get; set; }

        public IEnumerable<SelectListItem> Roles { get; set; }
    }

    public class ShortUserModel
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public int AvatarId { get; set; }
    }

    public class UserListModel
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public string Email { get; set; }

        [Display(Name = "Avatar")]
        public int AvatarId { get; set; }

        [Display(Name = "Regisration Date")]
        public DateTime RegisrationDate { get; set; }

        public string Role { get; set; }
    }
}