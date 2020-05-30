using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FirstAPI.WebClient.Startup))]
namespace FirstAPI.WebClient
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
