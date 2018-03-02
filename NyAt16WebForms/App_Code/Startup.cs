using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NyAt16WebForms.Startup))]
namespace NyAt16WebForms
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
