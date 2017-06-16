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
        private readonly IRepository<DalTopic> topicRepository;

        public CommentService(IRepository<DalComment> commentRepository,
            IRepository<DalTopic> topicRepository)
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

        public BllComment GetComment(int id)
        {
            return commentRepository
                .FirstOrDefault(c => c.Id == id)
                .ToBllComment();
        }

        public void UpdateComment(BllComment comment)
        {
            if (comment.IsAnswer)
            {
                comment.Topic.IsAnswered = comment.IsAnswer;
                topicRepository.Update(comment.Topic.ToDalTopic());
            }
            commentRepository.Update(comment.ToDalComment());
        }

        public void Dispose()
        {
            commentRepository.Dispose();
            topicRepository.Dispose();
        }
    }
}
