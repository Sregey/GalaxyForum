using System;
using System.Collections.Generic;
using System.Linq;
using ForumBll.Interface.Models;
using ForumBll.Interface.Services;
using ForumBll.Mappers;
using ForumDal.Interface.Models;
using ForumDal.Interface.Repositories;
using ForumBll.Logger;

namespace ForumBll.Services
{
    public class ImageService : IImageService
    {
        private ILogger logger;

        private readonly IRepository<DalImage> imageRepository;

        public ImageService(IRepository<DalImage> repository,
            ILogger logger)
        {
            this.imageRepository = repository;
            this.logger = logger;
        }

        public BllImage GetImage(int id)
        {
            try
            {
                return imageRepository
                    .First(i => i.Id == id)
                    .ToBllImage();
            }
            catch (InvalidOperationException e)
            {
                logger.Warn(e.Message);
                throw;
            }
        }

        public void Dispose()
        {
            imageRepository.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
