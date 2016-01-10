using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ManagePortfolio.Startup))]
namespace ManagePortfolio
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
