using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SingnalRAndIldentity.Startup))]
namespace SingnalRAndIldentity
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
