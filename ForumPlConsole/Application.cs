using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumDependencyResolver;
using Ninject;
using ForumBll.Interface.Services;

namespace ForumPlConsole
{
    static class Application
    {
        static void Main(string[] args)
        {
            var kernal = new StandardKernel();
            kernal.ConfigurateResolverConsole();

            var forum = new ForumConsoleApi(
                kernal.Get<IUserService>(),
                kernal.Get<IAccountService>(),
                kernal.Get<ITopicService>(),
                kernal.Get<ICommentService>(),
                kernal.Get<ISectionService>(),
                kernal.Get<IRoleService>());

            forum.Run();
            forum.Dispose();
        }
    }
}
