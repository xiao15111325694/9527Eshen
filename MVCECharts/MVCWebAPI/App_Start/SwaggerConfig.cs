using System.Web.Http;
using WebActivatorEx;
using MVCWebAPI;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace MVCWebAPI
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "MVCWebAPI");
                })
                .EnableSwaggerUi(c =>
                {
                    GetXmlCommentsPath();
                });

        }

        private static string GetXmlCommentsPath()
        {
            return $@"{System.AppDomain.CurrentDomain.BaseDirectory}\bin\WebApi.XML";
        }
    }
}
