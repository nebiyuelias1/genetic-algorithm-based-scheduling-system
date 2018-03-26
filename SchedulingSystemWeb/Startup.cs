using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SchedulingSystemWeb.Startup))]
namespace SchedulingSystemWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
