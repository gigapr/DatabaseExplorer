using System;
using System.Collections;
using System.Web.Mvc;
using DatabaseSchemaReader.ConnectionstringBuilder.Interfaces;
using DatabaseSchemaReader.Website.Factories.Interfaces;
using DatabaseSchemaReader.Website.Model;
using GigaWebSolution.DatabaseSchemaReader.Interfaces;

namespace DatabaseSchemaReader.Website.Controllers
{
    public class DatabaseExplorerController : Controller
    {
        public string Connectionstring
        {
            get { return Session["Connectionstring"].ToString(); }
            set { Session["Connectionstring"] = value; }
        }

        private readonly ISchemaReader _schemaReader;
        private readonly IConnectionstringBuilder _connectionstringBuilder;        
        private readonly IConnectionstringArgumentsMapperFactory _connectionstringArgumentsMapperFactory;

        public DatabaseExplorerController(ISchemaReader schemaReader, 
                                          IConnectionstringBuilder connectionstringBuilder, 
                                          IConnectionstringArgumentsMapperFactory connectionstringArgumentsMapperFactory)
        {
            _schemaReader = schemaReader;
            _connectionstringBuilder = connectionstringBuilder;
            _connectionstringArgumentsMapperFactory = connectionstringArgumentsMapperFactory;
        }

        public ActionResult Index()
        {
            var databaseTypes = GetDatabaseTypes();

            ViewData["DatabaseTypes"] = Json(databaseTypes);

            return View("Index");
        }

        public PartialViewResult DisplayTable(string tableName)
        {
            var table = _schemaReader.GetTable(Connectionstring, tableName);

            return PartialView("Table", table);
        }

        public ActionResult GetForeignKeyConnections()
        {
            var foreignKeys = _schemaReader.GetForeignKeys(Connectionstring);

            return Json(foreignKeys, JsonRequestBehavior.AllowGet);
        }

        [ValidateInput(false)]
        [AcceptVerbs(HttpVerbs.Post)]
        public PartialViewResult GetTablesList(DatabaseConnection databaseConnection)
        {
            try
            {
                var connectionstringArgumentMapper = _connectionstringArgumentsMapperFactory.Make(databaseConnection.DatabaseType);

                var connectionstringArguments = connectionstringArgumentMapper.Map(databaseConnection);

                Connectionstring = _connectionstringBuilder.BuildConnectionString(connectionstringArguments);

                var tables = _schemaReader.GetTablesName(Connectionstring);
              
                return PartialView("TablesList", tables);
            }
            catch (Exception exception)
            {
                var message = exception.Message;

                return PartialView("Error", message);
            }            
        }

        private static IEnumerable GetDatabaseTypes()
        {
            return new[] { "- Select -", "Access", "SqlServer" };
        }
    }
}