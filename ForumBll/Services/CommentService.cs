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

        public CommentService(IRepository<DalComment> repository)
        {
            this.commentRepository = repository;
        }

        public void AddComment(BllComment comment)
        {
            comment.Date = DateTime.Now;
            comment.Status = new BllStatus() { Id = 1 };
            commentRepository.Add(comment.ToDalComment());
        }
    }
}
