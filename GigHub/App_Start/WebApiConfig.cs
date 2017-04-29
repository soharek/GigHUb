using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Web.Http;

namespace GigHub
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            var jsonconfig = GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings;
            jsonconfig.ContractResolver = new CamelCasePropertyNamesContractResolver();
            jsonconfig.Formatting = Formatting.Indented;

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
