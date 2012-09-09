using DatabaseSchemaReader.Contract.BusinessObjects;
using DatabaseSchemaReader.Contract.BusinessObjects.Interfaces;
using DatabaseSchemaReader.Contract.Exceptions;
using DatabaseSchemaReader.Service.Interfaces;
using DatabaseSchemaReader.WebHost.Controllers;
using DatabaseSchemaReader.WebHost.Exceptions;
using DatabaseSchemaReader.WebHost.Mappers.Interfaces;
using NUnit.Framework;
using Rhino.Mocks;

namespace DatabaseSchemaReader.WebHost.Test.Attributes
{
    [TestFixture, Category("Unit")]
    public class SchemaExplorerExceptionHandlerAttributeTest
    {
        private DatabaseSchemaExplorerController  _databaseSchemaExplorerController;
        private IConnectionstringArgumentsMapper _connectionstringArgumentsMapper;
        private IDatabaseSchemaExplorer _databaseSchemaExplorer;

        [SetUp]
        public void SetUp()
        {
            _connectionstringArgumentsMapper = MockRepository.GenerateStub<IConnectionstringArgumentsMapper>();

            _databaseSchemaExplorer = MockRepository.GenerateStub<IDatabaseSchemaExplorer>();    
        }

        [Test, ExpectedException(typeof(TableNotFoundException))]
        public void Should_throw_an_exception_when_table_is_not_found()
        {
            const string tableName = "NotExsitingTable";
            
            _connectionstringArgumentsMapper.Expect(csam => csam.Map(Arg<string>.Is.Anything, Arg<string>.Is.Anything, Arg<string>.Is.Anything, Arg<string>.Is.Anything, Arg<string>.Is.Anything, Arg<string>.Is.Anything)).Return(new ConnectionstringArguments());
            
            _databaseSchemaExplorer.Expect(de => de.GetTable(Arg<IConnectionstringArguments>.Is.Anything, Arg<string>.Is.Anything)).Throw(new TableNotFoundException(tableName));

            _databaseSchemaExplorerController = new DatabaseSchemaExplorerController(_connectionstringArgumentsMapper, _databaseSchemaExplorer);

            _databaseSchemaExplorerController.Tables("DatabaseType", "Provider", "DataSource", "DatabaseName", tableName, "Username", "Password");
        }

        [Test, ExpectedException(typeof(DatabaseTypeException))]
        public void Should_throw_an_exception_when_databasetype_is_not_supported()
        {
            
            _connectionstringArgumentsMapper.Expect(cam => cam.Map(Arg<string>.Is.Anything, Arg<string>.Is.Anything, Arg<string>.Is.Anything, Arg<string>.Is.Anything, Arg<string>.Is.Anything, Arg<string>.Is.Anything))
                                            .Throw(new DatabaseTypeException("DatabaseTypeException"));
    
            _databaseSchemaExplorerController = new DatabaseSchemaExplorerController(_connectionstringArgumentsMapper, _databaseSchemaExplorer);

            _databaseSchemaExplorerController.Tables("DatabaseType", "Provider", "DataSource", "DatabaseName", "TableName","Username", "Password");
        }
    }
}