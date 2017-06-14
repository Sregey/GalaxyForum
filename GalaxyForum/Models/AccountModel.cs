using System.ComponentModel.DataAnnotations;

namespace GalaxyForum.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Login is required.")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Login length must be between 5 to 20 characters.")]
        [RegularExpression(@"\w*",
            ErrorMessage = "Loging must consists of Latin letters, digits and underscores. ")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [RegularExpression(@"(?i)\b[A-Z0-9._%-]+@[A-Z0-9.-]+\.[A-Z]{2,4}\b",
            ErrorMessage = "Email isn't correct.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Password length must be between 5 to 20 characters.")]
        [RegularExpression(@"[0-9a-zA-Z_!@#$%^&*]*",
            ErrorMessage = "Password must consists of Latin letters, digits, underscores and special symbols (!, @, #, $, %, ^, &, *).")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Сonfirm password is required.")]
        [Compare("Password", ErrorMessage = "Сonfirm password isn't equal to password.")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }
}
