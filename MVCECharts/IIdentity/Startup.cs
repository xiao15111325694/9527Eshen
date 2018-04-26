using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IIdentity.Startup))]
namespace IIdentity
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
