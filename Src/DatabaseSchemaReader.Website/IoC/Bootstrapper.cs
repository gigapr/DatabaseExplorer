using System.Configuration;
using System.Web.Mvc;
using DatabaseSchemaReader.Website.Configurations.Interfaces;
using Microsoft.Practices.Unity;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Embedded;
using Unity.Mvc3;

namespace DatabaseSchemaReader.Website.IoC
{
    public static class Bootstrapper
    {
        public static void Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            var websiteConfigurations = (IWebsiteConfigurations)ConfigurationManager.GetSection("websiteConfiguration");
            container.RegisterInstance(typeof(IWebsiteConfigurations), websiteConfigurations);

            var store = InitalizeRavenDbDocumentStore(websiteConfigurations);

            container.RegisterInstance(store);

            return container;          
        }

        private static IDocumentStore InitalizeRavenDbDocumentStore(IWebsiteConfigurations websiteConfigurations)
        {
            IDocumentStore store;

            if (websiteConfigurations.IsTestingEnviroment)
            {
                store = new EmbeddableDocumentStore
                {
                    DataDirectory = "Data",
                    RunInMemory = true,
                    UseEmbeddedHttpServer = true
                };
            }
            else
            {
                store = new DocumentStore
                {
                    ConnectionStringName = websiteConfigurations.ConnectionstringName
                };
            }

            store.Initialize();

            return store;
        }
    }
}