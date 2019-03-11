using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Catering.Startup))]
namespace Catering
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
