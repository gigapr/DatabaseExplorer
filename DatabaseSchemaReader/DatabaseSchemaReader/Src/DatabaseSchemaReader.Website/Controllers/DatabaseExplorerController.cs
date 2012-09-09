using System;
using System.Linq;
using System.Web.Mvc;
using DatabaseSchemaReader.ConnectionstringBuilder.Interfaces;
using DatabaseSchemaReader.Website.Mappers.Interfaces;
using DatabaseSchemaReader.Website.Models;
using GigaWebSolution.DatabaseSchemaReader.Interfaces;

namespace DatabaseSchemaReader.Website.Controllers
{
    public class DatabaseExplorerController : Controller
    {
        //private const string Connectionstring = "Provider=SQLOLEDB;Data Source=localhost;Initial Catalog=Blog;Integrated Security=SSPI;OLE DB Services=-4;";

        public string Connectionstring
        {
            get { return Session["Connectionstring"].ToString(); }
            set { Session["Connectionstring"] = value; }
        }

        private readonly ISchemaReader _schemaReader;
        private readonly IConnectionstringBuilder _connectionstringBuilder;
        private readonly IConnectionstringArgumentsMapper _connectionstringArgumentsMapper;

        public DatabaseExplorerController(ISchemaReader schemaReader, IConnectionstringBuilder connectionstringBuilder, IConnectionstringArgumentsMapper connectionstringArgumentsMapper)
        {
            _schemaReader = schemaReader;
            _connectionstringBuilder = connectionstringBuilder;
            _connectionstringArgumentsMapper = connectionstringArgumentsMapper;
        }

        public ActionResult Index()
        {
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

        public PartialViewResult GetTablesList(DatabaseConnection databaseConnection)
        {
            try
            {
                databaseConnection.Provider = "SQLOLEDB";
                var connectionstringArguments = _connectionstringArgumentsMapper.Map(databaseConnection);

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
    }
}