using System.Configuration;
using System.Web.Mvc;
using DatabaseSchemaReader.ConnectionstringBuilder.Factories;
using DatabaseSchemaReader.ConnectionstringBuilder.Factories.Interfaces;
using DatabaseSchemaReader.ConnectionstringBuilder.Interfaces;
using DatabaseSchemaReader.Website.Configurations.Interfaces;
using DatabaseSchemaReader.Website.Factories;
using DatabaseSchemaReader.Website.Factories.Interfaces;
using DatabaseSchemaReader.Website.Mappers;
using DatabaseSchemaReader.Website.Mappers.Interfaces;
using DatabaseSchemaReader.Website.Services;
using DatabaseSchemaReader.Website.Services.Interfaces;
using GigaWebSolution.DatabaseSchemaReader;
using GigaWebSolution.DatabaseSchemaReader.Interfaces;
using GigaWebSolution.DatabaseSchemaReader.Mappers;
using GigaWebSolution.DatabaseSchemaReader.Mappers.Interfaces;
using Microsoft.Practices.Unity;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Embedded;
using Unity.Mvc3;

namespace DatabaseSchemaReader.Website.IoC
{
    public static class Bootstrapper
    {
        public static IDocumentStore Store;

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
            container.RegisterType<IConnectionstringArgumentsMapper, SqlServerConnectionstringArgumentsMapper>();
            container.RegisterType<ISchemaReader, SchemaReader>();

            container.RegisterType<IConnectionstringBuilder, ConnectionstringBuilder.ConnectionstringBuilder>();
            container.RegisterType<IConnectionstringBuilderFactory, ConnectionstringBuilderFactory>();
            container.RegisterType<IConnectionstringArgumentsMapperFactory, ConnectionstringArgumentsMapperFactory>();

            container.RegisterType<IForeignKeyMapper, ForeignKeyMapper>();
            container.RegisterType<IColumnMapper, ColumnMapper>();
            container.RegisterType<IIndexMapper, IndexMapper>();
            container.RegisterType<IUserMapper, UserMapper>();

            container.RegisterType<IAccountService, AccountService>();
            
            var store = InitalizeRavenDbDocumentStore(websiteConfigurations);

            container.RegisterInstance(store);

            return container;          
        }

        private static IDocumentStore InitalizeRavenDbDocumentStore(IWebsiteConfigurations websiteConfigurations)
        {
            if (websiteConfigurations.IsTestingEnviroment)
            {
                Store = new EmbeddableDocumentStore
                {
                    DataDirectory = "Data",
                    RunInMemory = true,
                    UseEmbeddedHttpServer = true
                };
            }
            else
            {
                Store = new DocumentStore
                {
                    ConnectionStringName = websiteConfigurations.ConnectionstringName
                };
            }

            Store.Initialize();

            return Store;
        }
    }
}