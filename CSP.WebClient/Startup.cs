using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CSP.WebClient.Startup))]
namespace CSP.WebClient
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
