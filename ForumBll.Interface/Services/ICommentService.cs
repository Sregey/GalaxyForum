using System.Collections.Generic;
using ForumBll.Interface.Models;
using System;

namespace ForumBll.Interface.Services
{
    public interface ICommentService : IDisposable
    {
        void AddComment(BllComment comment);

        void UpdateComment(BllComment comment);

        void DeleteComment(int id);

        BllComment GetComment(int id);

        BllComment GetRawComment();

        IEnumerable<BllComment> GetComments();
    }
}
