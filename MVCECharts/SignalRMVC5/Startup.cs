using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SignalRMVC5.Startup))]
namespace SignalRMVC5
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
