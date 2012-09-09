using System.Web.Mvc;
using DatabaseSchemaReader.ConnectionstringBuilder.Factories;
using DatabaseSchemaReader.ConnectionstringBuilder.Factories.Interfaces;
using DatabaseSchemaReader.ConnectionstringBuilder.Interfaces;
using DatabaseSchemaReader.ConnectionstringBuilder.Validators;
using DatabaseSchemaReader.ConnectionstringBuilder.Validators.Interfaces;
using DatabaseSchemaReader.Website.Mappers;
using DatabaseSchemaReader.Website.Mappers.Interfaces;
using GigaWebSolution.DatabaseSchemaReader;
using GigaWebSolution.DatabaseSchemaReader.Interfaces;
using GigaWebSolution.DatabaseSchemaReader.Mappers;
using GigaWebSolution.DatabaseSchemaReader.Mappers.Interfaces;
using Microsoft.Practices.Unity;
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

            container.RegisterType<IConnectionstringArgumentsMapper, ConnectionstringArgumentsMapper>();
            container.RegisterType<IForeignKeyMapper, ForeignKeyMapper>();
            container.RegisterType<IColumnMapper, ColumnMapper>();
            container.RegisterType<IIndexMapper, IndexMapper>();

            container.RegisterType<ISchemaReader, SchemaReader>();

            container.RegisterType<IConnectionstringBuilder, ConnectionstringBuilder.ConnectionstringBuilder>();
            container.RegisterType<IConnectionstringBuilderFactory, ConnectionstringBuilderFactory>();
            container.RegisterType<IConnectionstringArgumentsValidator, ConnectionstringArgumentsValidator>();

            return container;          
        }
    }
}