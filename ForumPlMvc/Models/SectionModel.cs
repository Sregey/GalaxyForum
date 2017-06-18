using System.ComponentModel.DataAnnotations;

namespace ForumPlMvc.Models
{
    public class SectionModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
    }
}