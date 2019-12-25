using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ShopDemo.WebUI.Startup))]
namespace ShopDemo.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
