using System.Collections.Generic;
using ForumBll.Interface.Models;
using System;

namespace ForumBll.Interface.Services
{
    public interface IRoleService : IDisposable
    {
        IEnumerable<BllRole> GetAllRoles();
    }
}
