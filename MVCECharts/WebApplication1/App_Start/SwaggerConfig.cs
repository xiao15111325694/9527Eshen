using System.Web.Http;
using WebActivatorEx;
using WebApplication1;
using Swashbuckle.Application;
using WebApplication1.DLLHelp;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace WebApplication1
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                    {
                        c.OperationFilter<AddAuthorizationHeader>();
                        c.SingleApiVersion("v1", "WebApplication1");

                    })
                .EnableSwaggerUi(c =>
                    {

                    });
        }
    }
}
