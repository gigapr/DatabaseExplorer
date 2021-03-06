﻿using DatabaseSchemaReader.ConnectionstringBuilder.Factories;
using DatabaseSchemaReader.ConnectionstringBuilder.Factories.Interfaces;
using DatabaseSchemaReader.ConnectionstringBuilder.Interfaces;
using DatabaseSchemaReader.ConnectionstringBuilder.Validators;
using DatabaseSchemaReader.ConnectionstringBuilder.Validators.Interfaces;
using DatabaseSchemaReader.Service;
using DatabaseSchemaReader.Service.Interfaces;
using DatabaseSchemaReader.Website.Mappers;
using DatabaseSchemaReader.Website.Mappers.Interfaces;
using GigaWebSolution.DatabaseSchemaReader;
using GigaWebSolution.DatabaseSchemaReader.Interfaces;
using GigaWebSolution.DatabaseSchemaReader.Mappers;
using GigaWebSolution.DatabaseSchemaReader.Mappers.Interfaces;
using Microsoft.Practices.Unity;

namespace DatabaseSchemaReader.WebHost
{
    internal class IoC
    {
        private static bool _isConfigured;
        private static IUnityContainer _container;

        public static void RegisterTypes()
        {
            if (!_isConfigured)
            {
                _container = new UnityContainer();

                _container.RegisterType<IConnectionstringBuilder, ConnectionstringBuilder.ConnectionstringBuilder>();
                _container.RegisterType<IDatabaseSchemaExplorer, DatabaseSchemaExplorer>();
                _container.RegisterType<IUserMapper, UserMapper>();
                _container.RegisterType<ISchemaReader, SchemaReader>();
                _container.RegisterType<IConnectionstringBuilderFactory, ConnectionstringBuilderFactory>();
                _container.RegisterType<IForeignKeyMapper, ForeignKeyMapper>();
                _container.RegisterType<IColumnMapper, ColumnMapper>();
                _container.RegisterType<IIndexMapper, IndexMapper>();
            }

            _isConfigured = true;
        }

        public static T Resolve<T>() where T : class
        {
            if (!_isConfigured)
            {
                RegisterTypes();
            }

            return _container.Resolve<T>();
        }

        public static T Resolve<T>(string name)
        {
            if (!_isConfigured)
            {
                RegisterTypes();
            }

            return _container.Resolve<T>(name);
        }
    }    
}