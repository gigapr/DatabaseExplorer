using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace DatabaseSchemaReader.WebHost.App_Start
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapHttpRoute(
                name: "DefaultSecured",
                routeTemplate: "{controller}/{action}/{databaseType}/{provider}/{dataSource}/{databaseName}/{tableName}/{username}/{password}"
            );

            routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{action}/{databaseType}/{provider}/{dataSource}/{databaseName}/{username}/{password}",
                defaults: new {
                    username = RouteParameter.Optional,
                    password = RouteParameter.Optional
                }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}