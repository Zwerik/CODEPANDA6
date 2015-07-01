using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(_0PTRIGHT.Startup))]
namespace _0PTRIGHT
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
