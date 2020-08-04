using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HotelMgmt.Startup))]
namespace HotelMgmt
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
