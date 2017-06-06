using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GalaxyForum.Startup))]
namespace GalaxyForum
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
