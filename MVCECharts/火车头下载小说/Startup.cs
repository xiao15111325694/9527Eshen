using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(火车头下载小说.Startup))]
namespace 火车头下载小说
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
