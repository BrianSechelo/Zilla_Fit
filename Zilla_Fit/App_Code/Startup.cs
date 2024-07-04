using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Zilla_Fit.Startup))]
namespace Zilla_Fit
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
