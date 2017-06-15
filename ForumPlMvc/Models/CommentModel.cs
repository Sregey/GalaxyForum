using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ForumPlMvc.Models
{
    public class CommentModel
    {
        public string Text { get; set; }

        public DateTime Date { get; set; }

        public bool IsAnswer { get; set; }

        public ShortUserModel Sender { get; set; }
    }

    public class AddCommentModel
    {
        public int TopicId { get; set; }

        [Required(ErrorMessage = "Some text is required.")]
        [MinLength(5, ErrorMessage = "Minimum length is 5 characters.")]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }
    }
}