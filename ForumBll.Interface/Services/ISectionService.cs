using System.Collections.Generic;
using ForumBll.Interface.Models;
using System;

namespace ForumBll.Interface.Services
{
    public interface ISectionService : IDisposable
    {
        BllSection GetSection(int id);

        IEnumerable<BllSection> GetAllSections();

        void AddSection(BllSection section);

        void UpdateSection(BllSection section);

        void DeleteSection(int id);
    }
}
