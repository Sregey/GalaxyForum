using System.Data.Entity;
using ForumBll.Interface.Services;
using ForumBll.Services;
using ForumDal.Interface.Repositories;
using ForumDal.Interface.Models;
using ForumDal.Repositories;
using Ninject;
using Ninject.Web.Common;
using ForumOrm;
using ForumOrm.Models;

namespace ForumDependencyResolver
{
    public static class ResolverConfig
    {
        public static void ConfigurateResolverWeb(this IKernel kernel)
        {
            Configure(kernel, true);
        }

        public static void ConfigurateResolverConsole(this IKernel kernel)
        {
            Configure(kernel, false);
        }

        private static void Configure(IKernel kernel, bool isWeb)
        {
            if (isWeb)
            {
                //kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
                kernel.Bind<DbContext>().To<ForumDbContext>().InRequestScope();
            }
            else
            {
                //kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InSingletonScope();
                kernel.Bind<DbContext>().To<ForumDbContext>().InSingletonScope();
            }

            //Services
            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<ISectionService>().To<SectionService>();
            kernel.Bind<ITopicService>().To<TopicService>();
            kernel.Bind<IAccountService>().To<AccountService>();
            kernel.Bind<IImageService>().To<ImageService>();
            kernel.Bind<ICommentService>().To<CommentService>();

            //Repositories
            kernel.Bind<IRepository<DalUser>>().To<UserRepository>();
            kernel.Bind<IRepository<DalSection>>().To<Repository<DalSection, Section>>();
            kernel.Bind<IRepository<DalTopic>>().To<Repository<DalTopic, Topic>>();
            kernel.Bind<IRepository<DalImage>>().To<Repository<DalImage, Image>>();
            kernel.Bind<IRepository<DalComment>>().To<Repository<DalComment, Comment>>();

            //kernel.Bind<IRepository<DalUser>>().To<UserRepository>();
            //kernel.Bind<IUserRepository>().To<UserRepository>();
        }
    }
}
