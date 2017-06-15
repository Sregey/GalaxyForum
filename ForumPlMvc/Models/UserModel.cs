using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Web;
using ForumPlMvc.Models.Validations;

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
        public string Name { get; set; }

        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Display(Name = "Father name")]
        public string FatherName { get; set; }

        public string Profession { get; set; }

        [Display(Name = "Extra information")]
        public string ExtraInfo { get; set; }

        [ImageExtensions("jpg,jpeg,png", ErrorMessage = "Only jpg, jpeg, png extensions are available.")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase Avatar { get; set; }
    }

    public class ShortUserModel
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public int AvatarId { get; set; }
    }
}