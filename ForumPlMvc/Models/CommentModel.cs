using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ForumPlMvc.Models
{
    public class CommentModel
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public DateTime Date { get; set; }

        public bool IsAnswer { get; set; }

        public string Status { get; set; }

        public ShortUserModel Sender { get; set; }
    }

    public class AddEditCommentModel
    {
        public int Id { get; set; }

        public int TopicId { get; set; }

        [Required(ErrorMessage = "Some text is required.")]
        [MinLength(3, ErrorMessage = "Minimum length is 3 characters.")]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }

        public bool IsAnswer { get; set; }
    }
}