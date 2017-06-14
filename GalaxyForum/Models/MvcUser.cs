using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GalaxyForum.Models
{
    public class MvcUser
    {
        public int Id { get; set; }

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
        public string ConfirmPassword { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string FatherName { get; set; }

        public string Profession { get; set; }

        public string ExtraInfo { get; set; }

        public DateTime RegisrationDate { get; set; }

        public MvcRole Role { get; set; }
    }
}