using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(trelo2.Startup))]
namespace trelo2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
