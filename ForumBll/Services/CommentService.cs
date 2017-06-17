using System;
using System.Collections.Generic;
using System.Linq;
using ForumBll.Interface.Models;
using ForumBll.Interface.Services;
using ForumBll.Mappers;
using ForumDal.Interface.Models;
using ForumDal.Interface.Repositories;

namespace ForumBll.Services
{
    public class CommentService : ICommentService
    {
        private readonly IRepository<DalComment> commentRepository;
        private readonly ITopicRepository topicRepository;

        public CommentService(IRepository<DalComment> commentRepository,
            ITopicRepository topicRepository)
        {
            this.commentRepository = commentRepository;
            this.topicRepository = topicRepository;
        }

        public void AddComment(BllComment comment)
        {
            comment.Date = DateTime.Now;
            comment.Status = new BllStatus() { Id = 1 };
            commentRepository.Add(comment.ToDalComment());
        }

        public void DeleteComment(int id)
        {
            commentRepository.Delete(id);
        }

        public BllComment GetComment(int id)
        {
            return commentRepository
                .FirstOrDefault(c => c.Id == id)
                .ToBllComment();
        }

        public BllComment GetRawComment()
        {
            return commentRepository.FirstOrDefault(c => c.Status.Id == (int)StatusEnum.Raw)
               .ToBllComment();
        }

        public void UpdateComment(BllComment comment)
        {
            DalComment dalComment = commentRepository.FirstOrDefault(c => c.Id == comment.Id);
            dalComment.IsAnswer = comment.IsAnswer;
            dalComment.Text = comment.Text;
            dalComment.Status.Id = comment.Status.Id;

            dalComment.Topic.IsAnswered = dalComment.IsAnswer;
            topicRepository.Update(dalComment.Topic);

            commentRepository.Update(dalComment);
        }

        public IEnumerable<BllComment> GetComments()
        {
            return commentRepository.GetByPredicate(c => true)
                .Select(c => c.ToBllComment());
        }

        public void Dispose()
        {
            commentRepository.Dispose();
            topicRepository.Dispose();
        }
    }
}
