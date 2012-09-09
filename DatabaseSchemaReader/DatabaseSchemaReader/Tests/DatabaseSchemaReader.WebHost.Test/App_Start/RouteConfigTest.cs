using System.Web;
using System.Web.Routing;
using NUnit.Framework;
using Rhino.Mocks;

namespace DatabaseSchemaReader.WebHost.Test.App_Start
{
    [TestFixture, Category("Unit")]
    public class RouteConfigTest
    {
        private HttpContextBase _httpContextMock;
        private RouteCollection _routes;

        [SetUp]
        public void SetUp()
        {
            _routes = new RouteCollection();

            WebApiApplication.RegisterRoutes(_routes);

            _httpContextMock = MockRepository.GenerateStub<HttpContextBase>();
        }

        
        [Test]                                 
        public void Should_be_able_to_route_to_gettables_action_in_databaseschemaexplorercontroller()
        {
            const string url = "~/DatabaseSchemaExplorer/Tables/DatabaseType/Provider/DataSource/DatabaseName/Username/Password";

            _httpContextMock.Expect(c => c.Request.AppRelativeCurrentExecutionFilePath).Return(url);

            var routeData = _routes.GetRouteData(_httpContextMock);

            Assert.IsNotNull(routeData, "Should have found the route");
            Assert.AreEqual("DatabaseSchemaExplorer", routeData.Values["Controller"]);
            Assert.AreEqual("Tables", routeData.Values["action"]);
            Assert.AreEqual("DatabaseType", routeData.Values["databaseType"]);
            Assert.AreEqual("Provider", routeData.Values["provider"]);
            Assert.AreEqual("DataSource", routeData.Values["dataSource"]);
            Assert.AreEqual("DatabaseName", routeData.Values["databaseName"]);
            Assert.AreEqual("Username", routeData.Values["username"]);
            Assert.AreEqual("Password", routeData.Values["password"]);
        }

        [Test]
        public void Should_be_able_to_route_to_gettable_action_in_databaseschemaexplorercontroller()
        {
            const string url = "~/DatabaseSchemaExplorer/Tables/DatabaseType/Provider/DataSource/DatabaseName/TableName/Username/Password";

            _httpContextMock.Expect(c => c.Request.AppRelativeCurrentExecutionFilePath).Return(url);

            var routeData = _routes.GetRouteData(_httpContextMock);

            Assert.IsNotNull(routeData, "Should have found the route");
            Assert.AreEqual("DatabaseSchemaExplorer", routeData.Values["Controller"]);
            Assert.AreEqual("Tables", routeData.Values["action"]);
            Assert.AreEqual("DatabaseType", routeData.Values["databaseType"]);
            Assert.AreEqual("Provider", routeData.Values["provider"]);
            Assert.AreEqual("DataSource", routeData.Values["dataSource"]);
            Assert.AreEqual("DatabaseName", routeData.Values["databaseName"]);
            Assert.AreEqual("TableName", routeData.Values["tableName"]);
            Assert.AreEqual("Username", routeData.Values["username"]);
            Assert.AreEqual("Password", routeData.Values["password"]);
        }

        [Test]
        public void Should_be_able_to_route_to_gettablesname_action_in_databaseschemaexplorercontroller()
        {
            const string url = "~/DatabaseSchemaExplorer/TablesName/DatabaseType/Provider/DataSource/DatabaseName/Username/Password";

            _httpContextMock.Expect(c => c.Request.AppRelativeCurrentExecutionFilePath).Return(url);

            var routeData = _routes.GetRouteData(_httpContextMock);

            Assert.IsNotNull(routeData, "Should have found the route");
            Assert.AreEqual("DatabaseSchemaExplorer", routeData.Values["Controller"]);
            Assert.AreEqual("TablesName", routeData.Values["action"]);
            Assert.AreEqual("DatabaseType", routeData.Values["databaseType"]);
            Assert.AreEqual("Provider", routeData.Values["provider"]);
            Assert.AreEqual("DataSource", routeData.Values["dataSource"]);
            Assert.AreEqual("DatabaseName", routeData.Values["databaseName"]);
            Assert.AreEqual("Username", routeData.Values["username"]);
            Assert.AreEqual("Password", routeData.Values["password"]);
        }

        [Test]
        public void Should_be_able_to_route_to_getviewsname_action_in_databaseschemaexplorercontroller()
        {
            const string url = "~/DatabaseSchemaExplorer/ViewsName/DatabaseType/Provider/DataSource/DatabaseName";

            _httpContextMock.Expect(c => c.Request.AppRelativeCurrentExecutionFilePath).Return(url);

            var routeData = _routes.GetRouteData(_httpContextMock);

            Assert.IsNotNull(routeData, "Should have found the route");
            Assert.AreEqual("DatabaseSchemaExplorer", routeData.Values["Controller"]);
            Assert.AreEqual("ViewsName", routeData.Values["action"]);
            Assert.AreEqual("DatabaseType", routeData.Values["databaseType"]);
            Assert.AreEqual("Provider", routeData.Values["provider"]);
            Assert.AreEqual("DataSource", routeData.Values["dataSource"]);
            Assert.AreEqual("DatabaseName", routeData.Values["databaseName"]);
        }
    }
}