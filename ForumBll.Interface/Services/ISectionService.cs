using System.Collections.Generic;
using ForumBll.Interface.Models;

namespace ForumBll.Interface.Services
{
    public interface ISectionService
    {
        IEnumerable<BllSection> GetAllSections();

        void AddSection(BllSection section);

        void DeleteSection(int id);
    }
}
