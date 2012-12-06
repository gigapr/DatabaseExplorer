using System.IO;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using DatabaseSchemaReader.WebHost.App_Start;
using DatabaseSchemaReader.WebHost.Providers;

namespace DatabaseSchemaReader.WebHost
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterRoutes(RouteTable.Routes);
            
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            
            RegisterDocumentationProvider();
        }

        private static void RegisterDocumentationProvider()
        {
            var config = GlobalConfiguration.Configuration;

            config.Services.Replace(typeof (IDocumentationProvider), new ApiDocumentationProvider());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            RouteConfig.RegisterRoutes(routes);
        }        
    }
}

