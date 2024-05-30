using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Ketthucmon.Startup))]
namespace Ketthucmon
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
