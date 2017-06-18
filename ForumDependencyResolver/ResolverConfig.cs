using System.Data.Entity;
using ForumBll.Interface.Services;
using ForumBll.Services;
using ForumBll.Logger;
using ForumDal.Interface.Repositories;
using ForumDal.Interface.Models;
using ForumDal.Repositories;
using Ninject;
using Ninject.Activation;
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

        private static IUserService ServiceForProvider(IContext c)
        {
            return new UserService(new UserRepository(new ForumDbContext()), new NLoggerAdapter());
        }

        private static void Configure(IKernel kernel, bool isWeb)
        {
            if (isWeb)
            {
                kernel.Bind<DbContext>().To<ForumDbContext>().InRequestScope();
            }
            else
            {
                kernel.Bind<DbContext>().To<ForumDbContext>().InSingletonScope();
            }

            //Services
            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<IUserService>().ToMethod(ServiceForProvider).WhenTargetHas<SpecialServiceAttribute>();
            kernel.Bind<ISectionService>().To<SectionService>();
            kernel.Bind<ITopicService>().To<TopicService>();
            kernel.Bind<IAccountService>().To<AccountService>();
            kernel.Bind<IImageService>().To<ImageService>();
            kernel.Bind<ICommentService>().To<CommentService>();
            kernel.Bind<IRoleService>().To<RoleService>();

            //Repositories
            kernel.Bind<IRepository<DalUser>>().To<UserRepository>();
            kernel.Bind<IRepository<DalSection>>().To<Repository<DalSection, Section>>();
            //kernel.Bind<IRepository<DalTopic>>().To<Repository<DalTopic, Topic>>();
            kernel.Bind<ITopicRepository>().To<TopicRepository>();
            kernel.Bind<IRepository<DalImage>>().To<Repository<DalImage, Image>>();
            kernel.Bind<IRepository<DalComment>>().To<Repository<DalComment, Comment>>();
            kernel.Bind<IRepository<DalRole>>().To<Repository<DalRole, Role>>();

            //Logger
            kernel.Bind<ILogger>().To<NLoggerAdapter>().InSingletonScope();
        }
    }
}
