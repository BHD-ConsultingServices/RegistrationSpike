using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Registration.Web.Startup))]
namespace Registration.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
