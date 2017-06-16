using System.Collections.Generic;
using ForumBll.Interface.Models;
using System;

namespace ForumBll.Interface.Services
{
    public interface ICommentService : IDisposable
    {
        void AddComment(BllComment comment);

        void UpdateComment(BllComment comment);

        BllComment GetComment(int id);
    }
}
