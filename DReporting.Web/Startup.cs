using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DReporting.Web.Startup))]
namespace DReporting.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
