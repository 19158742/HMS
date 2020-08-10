using Microsoft.Owin;
using Owin;
using HotelMgmt.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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
