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
                Sender = bllComment.Sender.ToShortUserModel(),
            };
        }

        public static BllComment ToBllComment(this AddCommentModel comment)
        {
            return new BllComment
            {
                Text = comment.Text,
                Topic = new BllTopic() { Id = comment.TopicId},
            };
        }
    }
}