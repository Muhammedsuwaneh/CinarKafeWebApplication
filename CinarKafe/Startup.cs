using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CinarKafe.Startup))]
namespace CinarKafe
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
