using System.Collections.Generic;
using ForumBll.Interface.Models;


namespace ForumBll.Interface.Services
{
    public interface ICommentService
    {
        void AddComment(BllComment comment);
    }
}
