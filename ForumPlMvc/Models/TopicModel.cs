﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ForumPlMvc.Models
{
    public class CreateTopicModel
    {
        [Required(ErrorMessage = "Title is required.")]
        [MinLength(5, ErrorMessage = "Minimum length is 5 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Text is required.")]
        [MinLength(5, ErrorMessage = "Minimum length is 5 characters.")]
        public string Text { get; set; }
    }

    public class TopicListModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string AuthorLogin { get; set; }

        public DateTime Date { get; set; }

        public bool IsAnswered { get; set; }

        [Display(Name = "Section")]
        public string SectionName { get; set; }

        public int SectionId { get; set; }
    }

    public class TopicDitailsModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public ShortUserModel Author { get; set; }

        public DateTime Date { get; set; }

        public bool IsAnswered { get; set; }

        public IEnumerable<CommentModel> Comments { get; set; }
    }
}