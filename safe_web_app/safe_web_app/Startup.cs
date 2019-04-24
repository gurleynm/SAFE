using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(safe_web_app.Startup))]
namespace safe_web_app
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
