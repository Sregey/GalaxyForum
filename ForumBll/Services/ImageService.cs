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
    public class ImageService : IImageService
    {
        private readonly IRepository<DalImage> imageRepository;

        public ImageService(IRepository<DalImage> repository)
        {
            this.imageRepository = repository;
        }

        public BllImage GetImage(int id)
        {
            return imageRepository
                .FirstOrDefault(i => i.Id == id)
                .ToBllImage();
        }
    }
}
