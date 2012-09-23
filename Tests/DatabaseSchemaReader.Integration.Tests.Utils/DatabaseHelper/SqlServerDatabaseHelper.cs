namespace DatabaseSchemaReader.Integration.Tests.Utils.DatabaseHelper
{
    public class SqlServerDatabaseHelper
    {
        public static void InitializeDatabase()
        {
            using (var blogContext = new BlogContext())
            {
                blogContext.Database.Initialize(true);
                blogContext.Database.Connection.Close();
            }
        }

        public static void DropDatabase()
        {
            using (var blogContext = new BlogContext())
            {
                blogContext.Database.Delete();
            }
        }

        public static void CreateView()
        {
            using (var blogContext = new BlogContext())
            {
                const string command = "CREATE View Posts_Blog " +
                                       "AS " +
                                       "SELECT P.Title, P.DateCreated, P.Content, B.BloggerName " +
                                       "FROM Posts p " +
                                       "INNER JOIN Blogs B ON B.Id = P.BlogId ";

                blogContext.Database.ExecuteSqlCommand(command);
            }
        }

        public static void CreateUser(string username, string password)
        {
            using (var blogContext = new BlogContext())
            {
                var command = string.Format("IF  EXISTS (SELECT * FROM sys.server_principals WHERE name = N'{0}') " +
                                            "DROP LOGIN [{0}] " +
                                            "CREATE LOGIN [{0}] WITH PASSWORD=N'{1}', DEFAULT_DATABASE=[Blog], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=ON, CHECK_POLICY=ON " +
                                            "EXEC sys.sp_addsrvrolemember @loginame = N'{0}', @rolename = N'sysadmin' " +
                                            "EXEC sys.sp_addsrvrolemember @loginame = N'{0}', @rolename = N'securityadmin' " +
                                            "EXEC sys.sp_addsrvrolemember @loginame = N'{0}', @rolename = N'serveradmin' " +
                                            "EXEC sys.sp_addsrvrolemember @loginame = N'{0}', @rolename = N'setupadmin' " +
                                            "USE [Blog] " +
                                            "IF  EXISTS (SELECT * FROM sys.database_principals WHERE name = N'{0}') " +
                                            "DROP USER [{0}] " +
                                            "USE [Blog] " +
                                            "CREATE USER [{0}] FOR LOGIN [{0}] WITH DEFAULT_SCHEMA=[dbo] "
                                            , username, password);

                blogContext.Database.ExecuteSqlCommand(command);
            }
        }

        public static void DropView()
        {
            using (var blogContext = new BlogContext())
            {
                const string command = "IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[Posts_Blog]')) " +
                                       "DROP VIEW [dbo].[Posts_Blog]";

                blogContext.Database.ExecuteSqlCommand(command);
            }
        }
    }
}
