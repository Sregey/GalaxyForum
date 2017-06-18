using System;
using System.Collections.Generic;
using System.Linq;
using ForumBll.Interface.Models;
using ForumBll.Interface.Services;
using ForumBll.Mappers;
using ForumDal.Interface.Models;
using ForumDal.Interface.Repositories;
using ForumBll.Logger;

namespace ForumBll.Services
{
    public class CommentService : ICommentService
    {
        private ILogger logger;

        private readonly IRepository<DalComment> commentRepository;
        private readonly ITopicRepository topicRepository;

        public CommentService(IRepository<DalComment> commentRepository,
            ITopicRepository topicRepository,
            ILogger logger)
        {
            this.commentRepository = commentRepository;
            this.topicRepository = topicRepository;
            this.logger = logger;
        }

        public void AddComment(BllComment comment)
        {
            comment.Date = DateTime.Now;
            comment.Status = new BllStatus() { Id = 1 };

            try
            {
                commentRepository.Add(comment.ToDalComment());
            }
            catch (InvalidOperationException e)
            {
                logger.Warn(e.Message);
                throw;
            }
        }

        public void DeleteComment(int id)
        {
            try
            {
                commentRepository.Delete(id);
            }
            catch (InvalidOperationException e)
            {
                logger.Warn(e.Message);
                throw;
            }
        }

        public BllComment GetComment(int id)
        {
            try
            {
                return commentRepository
                    .First(c => c.Id == id)
                    .ToBllComment();
            }
            catch (InvalidOperationException e)
            {
                logger.Warn(e.Message);
                throw;
            }
        }

        public BllComment GetRawComment()
        {
            return commentRepository.FirstOrDefault(c => c.Status.Id == (int)StatusEnum.Raw)?
               .ToBllComment();
        }

        public void UpdateComment(BllComment comment)
        {
            DalComment dalComment = commentRepository.First(c => c.Id == comment.Id);
            dalComment.IsAnswer = comment.IsAnswer;
            dalComment.Text = comment.Text;
            dalComment.Status.Id = comment.Status.Id;

            dalComment.Topic.IsAnswered = dalComment.IsAnswer;
            topicRepository.Update(dalComment.Topic);


            try
            {
                topicRepository.Update(dalComment.Topic);
                commentRepository.Update(dalComment);
            }
            catch (InvalidOperationException e)
            {
                logger.Warn(e.Message);
                throw;
            }
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
