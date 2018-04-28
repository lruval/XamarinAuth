using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(MobileApps.MobileAppService.Startup))]

namespace MobileApps.MobileAppService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}