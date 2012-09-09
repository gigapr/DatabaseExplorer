using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Script.Serialization;
using DatabaseSchemaReader.ConnectionstringBuilder.Interfaces;
using DatabaseSchemaReader.Contract.BusinessObjects;
using DatabaseSchemaReader.Contract.BusinessObjects.Interfaces;
using DatabaseSchemaReader.Website.Controllers;
using DatabaseSchemaReader.Website.Mappers.Interfaces;
using GigaWebSolution.DatabaseSchemaReader.Interfaces;
using NUnit.Framework;
using Rhino.Mocks;

namespace DatabaseSchemaReader.Website.Test.Controllers
{
    [TestFixture, Category("Unit")]
    public class DatabaseExplorerControllerTest
    {
        private DatabaseExplorerController _databaseExplorerController;
        private ISchemaReader _schemaReader;
        private IConnectionstringBuilder _connectionstringBuilder;
        private HttpContextBase _context;
        private HttpSessionStateBase _session;
        private IConnectionstringArgumentsMapper _connectionstringArgumentsMapper;
        private const string Connectionstring = "Provider=SQLOLEDB;Data Source=localhost;Initial Catalog=Blog;Integrated Security=SSPI;OLE DB Services=-4;";   

        [SetUp]
        public void SetUp()
        {
            _session = MockRepository.GenerateStrictMock<HttpSessionStateBase>();
            _session.Stub(s => s["Connectionstring"]).Return(Connectionstring);

            _context = MockRepository.GenerateStrictMock<HttpContextBase>();
            _context.Stub(c => c.Session).Return(_session);

            _connectionstringBuilder = MockRepository.GenerateStub<IConnectionstringBuilder>();

            _connectionstringBuilder.Expect(cb => cb.BuildConnectionString(Arg<IConnectionstringArguments>.Is.Anything))
                                    .Return(Connectionstring);

            _connectionstringArgumentsMapper = MockRepository.GenerateMock<IConnectionstringArgumentsMapper>();

            _schemaReader = MockRepository.GenerateStub<ISchemaReader>();            
        }

        [Test]
        public void Index_action_returns_index_view()
        {
            const string expectedViewName = "Index";

            var result = _databaseExplorerController.Index() as ViewResult;

            Assert.IsNotNull(result, "Should have returned a ViewResult");
            Assert.AreEqual(expectedViewName, result.ViewName, "View name should have been {0}", expectedViewName);
        }

        [Test]
        public void Displaytable_should_return_table_partial_view()
        {
            const string tableName = "tableName";

            var table = new Table(tableName);
            
            _schemaReader.Expect(sr => sr.GetTable(Arg<string>.Is.Anything, Arg<string>.Is.Anything)).Return(table);

            _databaseExplorerController = new DatabaseExplorerController(_schemaReader, _connectionstringBuilder, _connectionstringArgumentsMapper);
            _databaseExplorerController.ControllerContext = new ControllerContext(_context, new RouteData(),_databaseExplorerController );

            var result = _databaseExplorerController.DisplayTable(tableName);

            Assert.IsNotNull(result);
            Assert.AreEqual("Table", result.ViewName);
            Assert.IsInstanceOf<Table>(result.ViewData.Model);
            Assert.AreEqual(table.Name, ((Table)result.ViewData.Model).Name);
        }

        [Test]
        public void Getforeignkeyconnections_should_return_table_tablesconnections_as_jason()
        {
            var foreignKeyColumn = new ForeignKeyColumn {Name = "foreignKeyColumn"};
            var primaryKeyColumn = new PrimaryKeyColumn { Name = "primaryKeyColumn" };

            var foreignKeys = new List<ForeignKey>
            {
                new ForeignKey
                {
                    ForeignKeyTableName = "ForeignKeyTableName",
                    PrimaryKeyTableName = "PrimaryKeyTableName",
                    ForeignKeyColumns = new List<ForeignKeyColumn> { foreignKeyColumn },
                    PrimaryKeysColumns = new List<PrimaryKeyColumn>{ primaryKeyColumn}
                }                          
            };

            _schemaReader.Expect(sr => sr.GetForeignKeys(Connectionstring)).Return(foreignKeys);

            _databaseExplorerController = new DatabaseExplorerController(_schemaReader, _connectionstringBuilder, _connectionstringArgumentsMapper);
            _databaseExplorerController.ControllerContext = new ControllerContext(_context, new RouteData(), _databaseExplorerController);

            var result = _databaseExplorerController.GetForeignKeyConnections() as JsonResult;

            Assert.IsNotNull(result);

            var serializer = new JavaScriptSerializer();
            var output = serializer.Serialize(result.Data);
            Console.WriteLine(output);

            Assert.AreEqual("[{\"Name\":null,\"OriginalName\":null,\"ForeignKeyTableName\":\"ForeignKeyTableName\",\"ForeignKeyTableSchema\":null,\"ForeignKeyColumns\":[{\"Name\":\"foreignKeyColumn\"}],\"PrimaryKeyTableName\":\"PrimaryKeyTableName\",\"PrimaryKeyTableSchema\":null,\"PrimaryKeysColumns\":[{\"Name\":\"primaryKeyColumn\"}]}]", output);
        }
    }
}