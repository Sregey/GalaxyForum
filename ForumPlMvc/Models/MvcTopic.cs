using System;
using System.ComponentModel.DataAnnotations;

namespace ForumPlMvc.Models
{
    public class MvcTopic
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [MinLength(5, ErrorMessage = "Minimum length is 5 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Text is required.")]
        [MinLength(5, ErrorMessage = "Minimum length is 5 characters.")]
        public string Text { get; set; }

        public DateTime Date { get; set; }

        public bool IsAnswered { get; set; }

        public MvcSection Section { get; set; }

        public MvcUser Author { get; set; }

        public MvcStatus Status { get; set; }
    }
}
