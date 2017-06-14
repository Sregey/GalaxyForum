using System.Collections.Generic;
using ForumBll.Interface.Models;

namespace ForumBll.Interface.Services
{
    public interface IImageService
    {
        BllImage GetImage(int id);
    }
}
