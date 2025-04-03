using System.Web.Http;
using WebActivatorEx;
using ApiRestFerreteria;
using Swashbuckle.Application;
using System.Collections.Generic;
using System.Linq;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace ApiRestFerreteria
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;
            var config = GlobalConfiguration.Configuration;

            if (config.Routes.Any(r => r.RouteTemplate == "swagger/docs/{apiVersion}"))
            {
                return; // Si ya existe la ruta, salimos
            }

            config.EnableSwagger(c =>
            {
                c.SingleApiVersion("v1", "ApiRestFerreteria");
                c.DescribeAllEnumsAsStrings();
            }).EnableSwaggerUi();
        }
    }
}
