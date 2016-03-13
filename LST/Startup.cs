using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LST.Startup))]
namespace LST
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
