using ForumPlMvc.Models;
using ForumBll.Interface.Models;

namespace ForumPlMvc.Infrastructure.Mappers
{
    public static class MvcUserMapper
    { 
        public static BllUser ToBllUser(this RegisterModel mvcUser)
        {
            return new BllUser()
            {
                Login = mvcUser.Login,
                Email = mvcUser.Email,
                Password = mvcUser.Password
            };
        }

        public static UserInfoModel ToUserInfoModel(this BllUser bllUser)
        {
            return new UserInfoModel()
            {
                Login = bllUser.Login,
                Email = bllUser.Email,
                Name = bllUser.Name,
                LastName = bllUser.LastName,
                FatherName = bllUser.FatherName,
                Profession = bllUser.Profession,
                ExtraInfo = bllUser.ExtraInfo,
                AvatarId = bllUser.Avatar.Id,
            };
        }

        public static void CopyToBllUser(this UserSettingModel settingModel, BllUser bllUser)
        {
            bllUser.Name = settingModel.Name;
            bllUser.LastName = settingModel.LastName;
            bllUser.FatherName = settingModel.FatherName;
            bllUser.Profession = settingModel.Profession;
            bllUser.ExtraInfo = settingModel.ExtraInfo;
            bllUser.Avatar = settingModel.Avatar.ToBllImage();
        }

        public static UserSettingModel ToUserSettingModel(this BllUser bllUser)
        {
            return new UserSettingModel()
            {
                Name = bllUser.Name,
                LastName = bllUser.LastName,
                FatherName = bllUser.FatherName,
                Profession = bllUser.Profession,
                ExtraInfo = bllUser.ExtraInfo
            };
        }

        public static ShortUserModel ToShortUserModel(this BllUser bllUser)
        {
            return new ShortUserModel()
            {
                Id = bllUser.Id,
                Login = bllUser.Login,
                AvatarId = bllUser.Avatar.Id,
            };
        }
    }
}