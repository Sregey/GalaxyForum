using System.Collections.Generic;
using ForumBll.Interface.Models;
using System;

namespace ForumBll.Interface.Services
{
    public interface IImageService : IDisposable
    {
        BllImage GetImage(int id);
    }
}
