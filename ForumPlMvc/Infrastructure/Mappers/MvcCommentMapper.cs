using ForumPlMvc.Models;
using ForumBll.Interface.Models;

namespace ForumPlMvc.Infrastructure.Mappers
{
    public static class MvcCommentMapper
    {
        public static CommentModel ToCommentModel(this BllComment bllComment)
        {
            return new CommentModel
            {
                Id = bllComment.Id,
                Text = bllComment.Text,
                Date = bllComment.Date,
                IsAnswer = bllComment.IsAnswer,
                Status = bllComment.Status.Name,
                Sender = bllComment.Sender.ToShortUserModel(),
            };
        }

        public static BllComment ToBllComment(this AddEditCommentModel comment)
        {
            return new BllComment
            {
                Id = comment.Id,
                Text = comment.Text,
                IsAnswer = comment.IsAnswer,
                Topic = new BllTopic() { Id = comment.TopicId},
            };
        }

        public static AddEditCommentModel ToAddEditCommentModel(this BllComment bllComment)
        {
            return new AddEditCommentModel
            {
                Id = bllComment.Id,
                Text = bllComment.Text,
                TopicId = bllComment.Topic.Id,
                IsAnswer = bllComment.IsAnswer,
            };
        }
    }
}