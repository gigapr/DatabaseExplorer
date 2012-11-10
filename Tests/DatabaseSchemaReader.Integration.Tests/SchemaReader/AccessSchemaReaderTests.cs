using System.Data.OleDb;
using System.Linq;
using DatabaseSchemaReader.Contract.BusinessObjects;
using GigaWebSolution.DatabaseSchemaReader.Interfaces;
using GigaWebSolution.DatabaseSchemaReader.Mappers;
using NUnit.Framework;

namespace DatabaseSchemaReader.Integration.Tests.SchemaReader
{
    [TestFixture, Category("Integration"), Ignore("Requires Microsoft Office Access database engine")]
    public class AccessSchemaReaderTests
    {
        private const string DatabseFilePath = @"..\..\Resources\Northwind.accdb";
        private readonly string _connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};", DatabseFilePath); 

        private ISchemaReader _databaseSchemaReader;
        
        [SetUp]
        public void SetUp()
        {
            var foreignKeyMapper = new ForeignKeyMapper();
            var columnMapper = new ColumnMapper();
            var indexMapper = new IndexMapper();

            _databaseSchemaReader = new GigaWebSolution.DatabaseSchemaReader.SchemaReader(foreignKeyMapper, columnMapper, indexMapper);
        }

        [Test]
        public void Should_be_able_to_get_a_list_of_tables_names()
        {
            var tableNames = _databaseSchemaReader.GetTablesName(_connectionString);

            Assert.IsNotNull(tableNames);
            Assert.AreEqual("Customers", tableNames.ElementAt(0));
            Assert.AreEqual("Employee Privileges", tableNames.ElementAt(1));
            Assert.AreEqual("Employees", tableNames.ElementAt(2));
            Assert.AreEqual("Inventory Transaction Types", tableNames.ElementAt(3));
            Assert.AreEqual("Inventory Transactions", tableNames.ElementAt(4));
            Assert.AreEqual("Invoices", tableNames.ElementAt(5));
            Assert.AreEqual("Order Details", tableNames.ElementAt(6));
            Assert.AreEqual("Order Details Status", tableNames.ElementAt(7));
            Assert.AreEqual("Orders", tableNames.ElementAt(8));
            Assert.AreEqual("Orders Status", tableNames.ElementAt(9));
            Assert.AreEqual("Orders Tax Status", tableNames.ElementAt(10));
            Assert.AreEqual("Privileges", tableNames.ElementAt(11));
            Assert.AreEqual("Products", tableNames.ElementAt(12));
            Assert.AreEqual("Purchase Order Details", tableNames.ElementAt(13));
            Assert.AreEqual("Purchase Order Status", tableNames.ElementAt(14));
            Assert.AreEqual("Purchase Orders", tableNames.ElementAt(15));
            Assert.AreEqual("Sales Reports", tableNames.ElementAt(16));
            Assert.AreEqual("Shippers", tableNames.ElementAt(17));
            Assert.AreEqual("Strings", tableNames.ElementAt(18));
            Assert.AreEqual("Suppliers", tableNames.ElementAt(19));
        }

        [Test]
        public void Should_be_able_to_get_a_list_of_views_names()
        {
            var views = _databaseSchemaReader.GetViewsName(_connectionString);

            Assert.IsNotNull(views);
            Assert.AreEqual(24, views.Count);
            Assert.AreEqual("Customers Extended", views.First());
        }

        [Test]
        public void Should_be_able_to_get_a_list_of_foreignkeys()
        {
            var foreignKeys = _databaseSchemaReader.GetForeignKeys(_connectionString);

            Assert.IsNotNull(foreignKeys);
            Assert.AreEqual(23, foreignKeys.Count());

            var first = foreignKeys.ElementAt(0);
            Assert.AreEqual("MSysNavPaneGroupCategoriesMSysNavPaneGroups", first.Name);
            Assert.AreEqual("MSysNavPaneGroupCategoriesMSysNavPaneGroups", first.OriginalName);
            Assert.AreEqual("MSysNavPaneGroups", first.ForeignKeyTableName);
            Assert.AreEqual("GroupCategoryID", first.ForeignKeyColumns.First().Name);
            Assert.AreEqual("MSysNavPaneGroupCategories", first.PrimaryKeyTableName);
            Assert.AreEqual("Id", first.PrimaryKeysColumns.First().Name);        
        }

        [Test]
        public void Should_be_able_to_get_table_info()
        {
            const string tableName = "Invoices";

            var tableInfo = _databaseSchemaReader.GetTable(_connectionString,  tableName);

            Assert.IsNotNull(tableInfo);
            Assert.AreEqual(7, tableInfo.Columns.Count);

            var firstColumnExpected = new Column
            {
                Name         = "Amount Due",
                OriginalName = "Amount Due",
                AllowNulls   = true,
                TypeInfo     = OleDbType.Currency,
                DefaultValue = "0",
                Precision    = 19,
                Scale        = 0,
                InPrimaryKey = false,
                IsIdentity   = false,
                Length       = -1
            };

            var firstColumn = tableInfo.Columns.ElementAt(0);
            Assert.AreEqual(firstColumnExpected.Name, firstColumn.Name);
            Assert.AreEqual(firstColumnExpected.OriginalName, firstColumn.OriginalName);
            Assert.AreEqual(firstColumnExpected.AllowNulls, firstColumn.AllowNulls);
            Assert.AreEqual(firstColumnExpected.TypeInfo, firstColumn.TypeInfo);
            Assert.AreEqual(firstColumnExpected.DefaultValue, firstColumn.DefaultValue);
            Assert.AreEqual(firstColumnExpected.Precision, firstColumn.Precision);
            Assert.AreEqual(firstColumnExpected.Scale, firstColumn.Scale);
            Assert.AreEqual(firstColumnExpected.InPrimaryKey, firstColumn.InPrimaryKey);
            Assert.AreEqual(firstColumnExpected.IsIdentity, firstColumn.IsIdentity);
            Assert.AreEqual(firstColumnExpected.Length, firstColumn.Length);

            var secondColumnExpected = new Column
            {
                Name         = "Due Date",
                OriginalName = "Due Date",
                AllowNulls   = true,
                TypeInfo     = OleDbType.Date,
                DefaultValue = "Null",
                Precision    = 0,
                Scale        = 0,
                InPrimaryKey = false,
                IsIdentity   = false,
                Length       = -1
            };

            var secondColumn = tableInfo.Columns.ElementAt(1);
            Assert.AreEqual(secondColumnExpected.Name, secondColumn.Name);
            Assert.AreEqual(secondColumnExpected.OriginalName, secondColumn.OriginalName);
            Assert.AreEqual(secondColumnExpected.AllowNulls, secondColumn.AllowNulls);
            Assert.AreEqual(secondColumnExpected.TypeInfo, secondColumn.TypeInfo);
            Assert.AreEqual(secondColumnExpected.DefaultValue, secondColumn.DefaultValue);
            Assert.AreEqual(secondColumnExpected.Precision, secondColumn.Precision);
            Assert.AreEqual(secondColumnExpected.Scale, secondColumn.Scale);
            Assert.AreEqual(secondColumnExpected.InPrimaryKey, secondColumn.InPrimaryKey);
            Assert.AreEqual(secondColumnExpected.IsIdentity, secondColumn.IsIdentity);
            Assert.AreEqual(secondColumnExpected.Length, secondColumn.Length);

            var thirdColumnExpected = new Column
            {
                Name         = "Invoice Date",
                OriginalName = "Invoice Date",
                AllowNulls   = true,
                TypeInfo     = OleDbType.Date,
                DefaultValue = "=Now()",
                Precision    = 0,
                Scale        = 0,
                InPrimaryKey = false,
                IsIdentity   = false,
                Length       = -1
            };

            var thirdColumn = tableInfo.Columns.ElementAt(2);
            Assert.AreEqual(thirdColumnExpected.Name, thirdColumn.Name);
            Assert.AreEqual(thirdColumnExpected.OriginalName, thirdColumn.OriginalName);
            Assert.AreEqual(thirdColumnExpected.AllowNulls, thirdColumn.AllowNulls);
            Assert.AreEqual(thirdColumnExpected.TypeInfo, thirdColumn.TypeInfo);
            Assert.AreEqual(thirdColumnExpected.DefaultValue, thirdColumn.DefaultValue);
            Assert.AreEqual(thirdColumnExpected.Precision, thirdColumn.Precision);
            Assert.AreEqual(thirdColumnExpected.Scale, thirdColumn.Scale);
            Assert.AreEqual(thirdColumnExpected.InPrimaryKey, thirdColumn.InPrimaryKey);
            Assert.AreEqual(thirdColumnExpected.IsIdentity, thirdColumn.IsIdentity);
            Assert.AreEqual(thirdColumnExpected.Length, thirdColumn.Length);

            var fourthColumnExpected = new Column
            {
                Name         = "Invoice ID",
                OriginalName = "Invoice ID",
                AllowNulls   = false,
                TypeInfo     = OleDbType.Integer,
                DefaultValue = "Null",
                Precision    = 10,
                Scale        = 0,
                InPrimaryKey = true,
                IsIdentity   = false,
                Length       = -1
            };

            var fourthColumn = tableInfo.Columns.ElementAt(3);
            Assert.AreEqual(fourthColumnExpected.Name, fourthColumn.Name);
            Assert.AreEqual(fourthColumnExpected.OriginalName, fourthColumn.OriginalName);
            Assert.AreEqual(fourthColumnExpected.AllowNulls, fourthColumn.AllowNulls);
            Assert.AreEqual(fourthColumnExpected.TypeInfo, fourthColumn.TypeInfo);
            Assert.AreEqual(fourthColumnExpected.DefaultValue, fourthColumn.DefaultValue);
            Assert.AreEqual(fourthColumnExpected.Precision, fourthColumn.Precision);
            Assert.AreEqual(fourthColumnExpected.Scale, fourthColumn.Scale);
            Assert.AreEqual(fourthColumnExpected.InPrimaryKey, fourthColumn.InPrimaryKey);
            Assert.AreEqual(fourthColumnExpected.IsIdentity, fourthColumn.IsIdentity);
            Assert.AreEqual(fourthColumnExpected.Length, fourthColumn.Length);


            var fifthColumnExpected = new Column
            {
                Name         = "Order ID",
                OriginalName = "Order ID",
                AllowNulls   = true,
                TypeInfo     = OleDbType.Integer,
                DefaultValue = "Null",
                Precision    = 10,
                Scale        = 0,
                InPrimaryKey = false,
                IsIdentity   = false,
                Length       = -1
            };

            var fiftthColumn = tableInfo.Columns.ElementAt(4);
            Assert.AreEqual(fifthColumnExpected.Name, fiftthColumn.Name);
            Assert.AreEqual(fifthColumnExpected.OriginalName, fiftthColumn.OriginalName);
            Assert.AreEqual(fifthColumnExpected.AllowNulls, fiftthColumn.AllowNulls);
            Assert.AreEqual(fifthColumnExpected.TypeInfo, fiftthColumn.TypeInfo);
            Assert.AreEqual(fifthColumnExpected.DefaultValue, fiftthColumn.DefaultValue);
            Assert.AreEqual(fifthColumnExpected.Precision, fiftthColumn.Precision);
            Assert.AreEqual(fifthColumnExpected.Scale, fiftthColumn.Scale);
            Assert.AreEqual(fifthColumnExpected.InPrimaryKey, fiftthColumn.InPrimaryKey);
            Assert.AreEqual(fifthColumnExpected.IsIdentity, fiftthColumn.IsIdentity);
            Assert.AreEqual(fifthColumnExpected.Length, fiftthColumn.Length);

            var sixthColumnExpected = new Column
            {
                Name         = "Shipping",
                OriginalName = "Shipping",
                AllowNulls   = true,
                TypeInfo     = OleDbType.Currency,
                DefaultValue = "0",
                Precision    = 19,
                Scale        = 0,
                InPrimaryKey = false,
                IsIdentity   = false,
                Length       = -1
            };

            var sixthColumn = tableInfo.Columns.ElementAt(5);
            Assert.AreEqual(sixthColumn.Name, sixthColumnExpected.Name);
            Assert.AreEqual(sixthColumn.OriginalName, sixthColumnExpected.OriginalName);
            Assert.AreEqual(sixthColumn.AllowNulls, sixthColumnExpected.AllowNulls);
            Assert.AreEqual(sixthColumn.TypeInfo, sixthColumnExpected.TypeInfo);
            Assert.AreEqual(sixthColumn.DefaultValue, sixthColumnExpected.DefaultValue);
            Assert.AreEqual(sixthColumn.Precision, sixthColumnExpected.Precision);
            Assert.AreEqual(sixthColumn.Scale, sixthColumnExpected.Scale);
            Assert.AreEqual(sixthColumn.InPrimaryKey, sixthColumnExpected.InPrimaryKey);
            Assert.AreEqual(sixthColumn.IsIdentity, sixthColumnExpected.IsIdentity);
            Assert.AreEqual(sixthColumn.Length, sixthColumnExpected.Length);

            var seventhColumnExpected = new Column
            {
                Name         = "Tax",
                OriginalName = "Tax",
                AllowNulls   = true,
                TypeInfo     = OleDbType.Currency,
                DefaultValue = "0",
                Precision    = 19,
                Scale        = 0,
                InPrimaryKey = false,
                IsIdentity   = false,
                Length       = -1
            };

            var seventhColumn = tableInfo.Columns.ElementAt(6);
            Assert.AreEqual(seventhColumn.Name, seventhColumnExpected.Name);
            Assert.AreEqual(seventhColumn.OriginalName, seventhColumnExpected.OriginalName);
            Assert.AreEqual(seventhColumn.AllowNulls, seventhColumnExpected.AllowNulls);
            Assert.AreEqual(seventhColumn.TypeInfo, seventhColumnExpected.TypeInfo);
            Assert.AreEqual(seventhColumn.DefaultValue, seventhColumnExpected.DefaultValue);
            Assert.AreEqual(seventhColumn.Precision, seventhColumnExpected.Precision);
            Assert.AreEqual(seventhColumn.Scale, seventhColumnExpected.Scale);
            Assert.AreEqual(seventhColumn.InPrimaryKey, seventhColumnExpected.InPrimaryKey);
            Assert.AreEqual(seventhColumn.IsIdentity, seventhColumnExpected.IsIdentity);
            Assert.AreEqual(seventhColumn.Length, seventhColumnExpected.Length);

            var foreIgnKeys = tableInfo.ForeignKeys;

            Assert.AreEqual(1, foreIgnKeys.Count);
            Assert.AreEqual("Order ID", foreIgnKeys.First().ForeignKeyColumns.First().Name);
            Assert.AreEqual("Orders", foreIgnKeys.First().PrimaryKeyTableName);
        }

        [Test] 
        public void Should_be_able_to_get_a_list_of_tables()
        {
            var tables = _databaseSchemaReader.GetTables(_connectionString);

            Assert.AreEqual(20, tables.Count);
        }
    }
}
