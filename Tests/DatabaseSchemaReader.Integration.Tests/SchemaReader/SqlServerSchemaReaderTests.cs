using System.Data.OleDb;
using System.Linq;
using DatabaseSchemaReader.Contract.BusinessObjects;
using DatabaseSchemaReader.Integration.Tests.Utils.DatabaseHelper;
using GigaWebSolution.DatabaseSchemaReader.Interfaces;
using GigaWebSolution.DatabaseSchemaReader.Mappers;
using NUnit.Framework;

namespace DatabaseSchemaReader.Integration.Tests.SchemaReader
{
    [TestFixture, Category("Integration")]
    public class SqlServerSchemaReaderTests
    {
        private ISchemaReader _databaseSchemaReader;
        private const string ConnectionString = "Provider=SQLOLEDB;Data Source=localhost;Initial Catalog=Blog;Integrated Security=SSPI;OLE DB Services=-4;"; 

        [SetUp]
        public void SetUp()
        {
            SqlServerDatabaseHelper.InitializeDatabase();

            InitializeDatabaseSchemaReader();
        }

        [Test]
        public void Should_be_able_to_get_a_list_of_tables_names()
        {
            var tableNames = _databaseSchemaReader.GetTablesName(ConnectionString);

            Assert.IsNotNull(tableNames);
            Assert.AreEqual("Authors", tableNames.ElementAt(0));
            Assert.AreEqual("Blogs", tableNames.ElementAt(1));
            Assert.AreEqual("Comments", tableNames.ElementAt(2));
            Assert.AreEqual("Posts", tableNames.ElementAt(3));
        }

        [Test]
        public void Should_be_able_to_get_a_list_of_views_names()
        {
            SqlServerDatabaseHelper.CreateView();

            var views = _databaseSchemaReader.GetViewsName(ConnectionString);

            Assert.IsNotNull(views);
            Assert.AreEqual(1, views.Count);
            Assert.AreEqual("Posts_Blog", views.First());

            SqlServerDatabaseHelper.DropView();
        }

        [Test]
        public void Should_be_able_to_get_a_list_of_foreignkeys()
        {
            SqlServerDatabaseHelper.CreateView();

            var foreignKeys = _databaseSchemaReader.GetForeignKeys(ConnectionString);

            Assert.IsNotNull(foreignKeys);
            Assert.AreEqual(3, foreignKeys.Count());

            var first = foreignKeys.ElementAt(0);
            Assert.AreEqual("FK_Comments_Posts_PostId", first.Name);
            Assert.AreEqual("FK_Comments_Posts_PostId", first.OriginalName);
            Assert.AreEqual("Comments", first.ForeignKeyTableName);
            Assert.AreEqual("PostId", first.ForeignKeyColumns.First().Name);
            Assert.AreEqual("Posts", first.PrimaryKeyTableName);
            Assert.AreEqual("Id", first.PrimaryKeysColumns.First().Name);

            var second = foreignKeys.ElementAt(1);
            Assert.AreEqual("FK_Posts_Authors_Author_Name_Author_Surname", second.Name);
            Assert.AreEqual("FK_Posts_Authors_Author_Name_Author_Surname", second.OriginalName);
            Assert.AreEqual("Posts", second.ForeignKeyTableName);
            Assert.AreEqual("Author_Name", second.ForeignKeyColumns.First().Name);
            Assert.AreEqual("Authors", second.PrimaryKeyTableName);
            Assert.AreEqual("Name", second.PrimaryKeysColumns.First().Name);

            var third = foreignKeys.ElementAt(2);
            Assert.AreEqual("FK_Posts_Blogs_BlogId", third.Name);
            Assert.AreEqual("FK_Posts_Blogs_BlogId", third.OriginalName);
            Assert.AreEqual("Posts", third.ForeignKeyTableName);
            Assert.AreEqual("BlogId", third.ForeignKeyColumns.First().Name);
            Assert.AreEqual("Blogs", third.PrimaryKeyTableName);
            Assert.AreEqual("Id", third.PrimaryKeysColumns.First().Name);

            SqlServerDatabaseHelper.DropView();
        }

        [Test]
        public void Should_return_an_empty_list_when_no_view_exsists()
        {
            var viewNames = _databaseSchemaReader.GetViewsName(ConnectionString);

            Assert.IsNotNull(viewNames);
            Assert.AreEqual(0, viewNames.Count);
        }  

        [Test]
        public void Should_be_able_to_get_table_info()
        {
            const string tableName = "Posts";

            var tableInfo = _databaseSchemaReader.GetTable(ConnectionString,  tableName);

            Assert.IsNotNull(tableInfo);
            Assert.AreEqual(7, tableInfo.Columns.Count);

            var firstColumnExpected = new Column
            {
                Name         = "Id",
                OriginalName = "Id",
                AllowNulls   = false,
                TypeInfo     = OleDbType.Integer,
                DefaultValue = "Null",
                Precision    = 10,
                Scale        = 0,
                InPrimaryKey = true,
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
                Name         = "Title",
                OriginalName = "Title",
                AllowNulls   = true,
                TypeInfo     = OleDbType.WChar,
                DefaultValue = "Null",
                Precision    = 0,
                Scale        = 0,
                InPrimaryKey = false,
                IsIdentity   = false,
                Length       = 1073741823
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
                Name         = "DateCreated",
                OriginalName = "DateCreated",
                AllowNulls   = false,
                TypeInfo     = OleDbType.DBTimeStamp,
                DefaultValue = "Null",
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
                Name         = "Content",
                OriginalName = "Content",
                AllowNulls   = true,
                TypeInfo     = OleDbType.WChar,
                DefaultValue = "Null",
                Precision    = 0,
                Scale        = 0,
                InPrimaryKey = false,
                IsIdentity   = false,
                Length       = 1073741823
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
                Name = "BlogId",
                OriginalName = "BlogId",
                AllowNulls = false,
                TypeInfo = OleDbType.Integer,
                DefaultValue = "Null",
                Precision = 10,
                Scale = 0,
                InPrimaryKey = false,
                IsIdentity = false,
                Length = -1
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
                Name = "Author_Name",
                OriginalName = "Author_Name",
                AllowNulls = true,
                TypeInfo = OleDbType.WChar,
                DefaultValue = "Null",
                Precision = 0,
                Scale = 0,
                InPrimaryKey = false,
                IsIdentity = false,
                Length = 128
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
                Name = "Author_Surname",
                OriginalName = "Author_Surname",
                AllowNulls = true,
                TypeInfo = OleDbType.WChar,
                DefaultValue = "Null",
                Precision = 0,
                Scale = 0,
                InPrimaryKey = false,
                IsIdentity = false,
                Length = 128
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

            Assert.AreEqual(2, foreIgnKeys.Count);
            Assert.AreEqual("Author_Name", foreIgnKeys.First().ForeignKeyColumns.First().Name);
            Assert.AreEqual("Authors", foreIgnKeys.First().PrimaryKeyTableName);
        }

        [Test] public void Should_be_able_to_get_a_list_of_tables()
        {
            var tables = _databaseSchemaReader.GetTables(ConnectionString);

            Assert.AreEqual(4, tables.Count);
        }

        [TearDown]
        public void TearDown()
        {
           // SqlServerDatabaseHelper.DropDatabase();
        }

        private void InitializeDatabaseSchemaReader()
        {
            var foreignKeyMapper = new ForeignKeyMapper();
            var columnMapper = new ColumnMapper();
            var indexMapper = new IndexMapper();

            _databaseSchemaReader = new GigaWebSolution.DatabaseSchemaReader.SchemaReader(foreignKeyMapper, columnMapper, indexMapper);
        }
    }
}
