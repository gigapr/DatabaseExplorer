using System.Web.Mvc;
using DatabaseSchemaReader.Website.Controllers;
using NUnit.Framework;

namespace DatabaseSchemaReader.Website.Test.Controllers
{
    [TestFixture]
    public class HomeControllerTest
    {
        private HomeController _homeController;

        [SetUp]
        public void SetUp()
        {
            _homeController = new HomeController();
        }

        [Test]
        public void Index_action_returns_index_view()
        {
            const string expectedViewName = "Index";

            var result = _homeController.Index() as ViewResult;

            Assert.IsNotNull(result, "Should have returned a ViewResult");
            Assert.AreEqual(expectedViewName, result.ViewName, "View name should have been {0}", expectedViewName);
        }
    }
}