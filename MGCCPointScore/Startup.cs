using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MGCCPointScore.Startup))]
namespace MGCCPointScore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
