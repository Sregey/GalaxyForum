using System.Collections.Generic;
using ForumBll.Interface.Models;

namespace ForumBll.Interface.Services
{
    public interface ISectionService
    {
        int Count { get; }

        BllSection GetSection(int id);

        IEnumerable<BllSection> GetAllSections();

        void AddSection(BllSection section);

        void DeleteSection(int id);
    }
}
