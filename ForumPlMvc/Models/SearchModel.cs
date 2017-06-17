using System.ComponentModel.DataAnnotations;

namespace ForumPlMvc.Models
{
    public class TopicSearchModel
    {
        public int SectionId { get; set; }

        [Required(ErrorMessage = "Text is required.")]
        public string Text { get; set; }
    }
}