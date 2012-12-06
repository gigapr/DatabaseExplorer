using System.Collections.Generic;
using System.Linq;
using DatabaseSchemaReader.ConnectionstringBuilder.Interfaces;
using DatabaseSchemaReader.Contract.BusinessObjects;
using DatabaseSchemaReader.Contract.BusinessObjects.Interfaces;
using DatabaseSchemaReader.Service.Interfaces;
using DatabaseSchemaReader.WebHost.Controllers;
using DatabaseSchemaReader.WebHost.Mappers.Interfaces;
using GigaWebSolution.DatabaseSchemaReader.Interfaces;
using NUnit.Framework;
using Rhino.Mocks;

namespace DatabaseSchemaReader.WebHost.Test.Controllers
{
    [TestFixture, Category("Unit")]
    public class DatabaseSchemaExplorerControllerTests
    {
        private DatabaseSchemaExplorerController _databaseSchemaExplorerController;
        private IConnectionstringBuilder _connectionstringBuilder;
        private IConnectionstringArgumentsMapper _connectionstringArgumentsMapper;
        private string _connectionstring;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _connectionstringBuilder = MockRepository.GenerateStub<IConnectionstringBuilder>();

            _connectionstring = "validConnectionstring";
            _connectionstringBuilder.Expect(csb => csb.BuildConnectionString(Arg<SqlServerConnectionstringArguments>.Is.Anything)).Return(_connectionstring);

            _connectionstringArgumentsMapper = MockRepository.GenerateStub<IConnectionstringArgumentsMapper>();
            _connectionstringArgumentsMapper.Expect(csa => csa.Map(Arg<string>.Is.Anything, Arg<string>.Is.Anything, Arg<string>.Is.Anything, Arg<string>.Is.Anything, Arg<string>.Is.Anything, Arg<string>.Is.Anything)).Return(new SqlServerConnectionstringArguments());
        }

        [Test]
        public void Should_be_able_to_get_a_list_of_tables_name()
        {
            var schemaReader = MockRepository.GenerateStub<ISchemaReader>();
            schemaReader.Expect(sr => sr.GetTablesName(_connectionstring)).Return(new List<string>());

            var databaseSchemaExplorer = MockRepository.GenerateMock<IDatabaseSchemaExplorer>();
            databaseSchemaExplorer.Expect(de => de.GetTablesName(Arg<IConnectionstringArguments>.Is.Anything)).Return(new List<string>().AsQueryable());

            _databaseSchemaExplorerController = new DatabaseSchemaExplorerController(_connectionstringArgumentsMapper, databaseSchemaExplorer);

            var tablesName = _databaseSchemaExplorerController.TablesName("SqlServer", "Provider", "DataSource", "DatabaseName", "Username", "Password");

            Assert.IsNotNull(tablesName);
        }

        [Test]
        public void Should_be_able_to_get_a_table_info()
        {
            var schemaReader = MockRepository.GenerateStub<ISchemaReader>();

            const string tableName = "tableName";
            schemaReader.Expect(sr => sr.GetTable(_connectionstring, tableName)).Return(new Table(tableName));

            var databaseSchemaExplorer = MockRepository.GenerateMock<IDatabaseSchemaExplorer>();
            databaseSchemaExplorer.Expect(de => de.GetTable(Arg<IConnectionstringArguments>.Is.Anything, Arg<string>.Is.Anything)).Return(new Table("TableName"));

            _databaseSchemaExplorerController = new DatabaseSchemaExplorerController(_connectionstringArgumentsMapper, databaseSchemaExplorer);

            var table = _databaseSchemaExplorerController.Tables("SqlServer", "Provider", "DataSource", "DatabaseName", tableName, "Username", "Password");

            Assert.IsNotNull(table);
        }

        [Test]
        public void Should_be_able_to_get_a_list_of_tables_info()
        {
            var schemaReader = MockRepository.GenerateStub<ISchemaReader>();
         
            schemaReader.Expect(sr => sr.GetTables(_connectionstring)).Return(new List<Table>());

            var databaseSchemaExplorer = MockRepository.GenerateMock<IDatabaseSchemaExplorer>();
            databaseSchemaExplorer.Expect(de => de.GetTables(Arg<IConnectionstringArguments>.Is.Anything)).Return(new List<Table>().AsQueryable());

            _databaseSchemaExplorerController = new DatabaseSchemaExplorerController(_connectionstringArgumentsMapper, databaseSchemaExplorer);

            var result = _databaseSchemaExplorerController.Tables("SqlServer", "Provider", "DataSource", "DatabaseName", "Username", "Password");

            Assert.IsNotNull(result);
        }

        [Test]
        public void Should_be_able_to_get_a_list_of_view_names()
        {
            var schemaReader = MockRepository.GenerateStub<ISchemaReader>();
            schemaReader.Expect(sr => sr.GetViewsName(_connectionstring)).Return(new List<string>());

            var databaseSchemaExplorer = MockRepository.GenerateMock<IDatabaseSchemaExplorer>();
            databaseSchemaExplorer.Expect(de => de.GetViewsName(Arg<IConnectionstringArguments>.Is.Anything)).Return(new List<string>().AsQueryable());

            _databaseSchemaExplorerController = new DatabaseSchemaExplorerController(_connectionstringArgumentsMapper, databaseSchemaExplorer);

            var viewNames = _databaseSchemaExplorerController.ViewsName("SqlServer", "Provider", "DataSource", "DatabaseName", "Username", "Password");

            Assert.IsNotNull(viewNames);
        }
    }
}