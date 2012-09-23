using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;
using DatabaseSchemaReader.ConnectionstringBuilder.Interfaces;
using DatabaseSchemaReader.Website.Model;
using DatabaseSchemaReader.Website.Mappers.Interfaces;
using DatabaseSchemaReader.Website.ViewModels;
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
        private readonly IConnectionstringArgumentsMapper _connectionstringArgumentsMapper;

        public DatabaseExplorerController(ISchemaReader schemaReader, IConnectionstringBuilder connectionstringBuilder, IConnectionstringArgumentsMapper connectionstringArgumentsMapper)
        {
            _schemaReader = schemaReader;
            _connectionstringBuilder = connectionstringBuilder;
            _connectionstringArgumentsMapper = connectionstringArgumentsMapper;
        }

        public ActionResult Index()
        {
            var databaseConnection = new DatabaseConnection { DatabaseType = new SelectList(GetDatabaseTypes(), "key", "value") };

            var databaseExplorer = new DatabaseExplorer {DatabaseConnection = databaseConnection};  

            return View("Index", databaseExplorer);
        }

        private IEnumerable GetDatabaseTypes()
        {
            return new Dictionary<string, string>
            {
                { "Access", "Access"},
                { "SqlServer", "Sql Server"},
            };
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
                databaseConnection.Provider = "Microsoft.ACE.OLEDB.12.0";

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