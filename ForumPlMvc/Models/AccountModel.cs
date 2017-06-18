using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ForumPlMvc.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Login is required.")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Login length must be between 5 to 20 characters.")]
        [Remote("CheckLogin", "Account", ErrorMessage = "Login already exists.")]
        [RegularExpression(@"\w*",
            ErrorMessage = "Loging must consists of Latin letters, digits and underscores. ")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [Remote("CheckEmail", "Account", ErrorMessage = "Email already exists.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Password length must be between 5 to 20 characters.")]
        [RegularExpression(@"[0-9a-zA-Z_!@#$%^&*]*",
            ErrorMessage = "Password must consists of Latin letters, digits, underscores and special symbols (!, @, #, $, %, ^, &, *).")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Сonfirm password is required.")]
        [System.ComponentModel.DataAnnotations.
            Compare("Password", ErrorMessage = "Сonfirm password isn't equal to password.")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        [Required(ErrorMessage = "Email is required.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class ChangePasswordModel
    {
        [Required(ErrorMessage = "Old password is required.")]
        [Display(Name = "Old Password")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "New password is required.")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Password length must be between 5 to 20 characters.")]
        [RegularExpression(@"[0-9a-zA-Z_!@#$%^&*]*",
            ErrorMessage = "Password must consists of Latin letters, digits, underscores and special symbols (!, @, #, $, %, ^, &, *).")]
        [Display(Name = "New Password")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Сonfirm new password is required.")]
        [System.ComponentModel.DataAnnotations.
            Compare("NewPassword", ErrorMessage = "Сonfirm new password isn't equal to new password.")]
        [Display(Name = "Confirm New Password")]
        [DataType(DataType.Password)]
        public string ConfirmNewPassword { get; set; }
    }
}
