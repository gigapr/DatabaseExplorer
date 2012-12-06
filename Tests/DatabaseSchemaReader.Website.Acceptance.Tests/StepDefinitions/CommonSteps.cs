using CassiniDev;
using DatabaseSchemaReader.Website.Acceptance.Tests.StepHelpers;
using TechTalk.SpecFlow;

namespace DatabaseSchemaReader.Website.Acceptance.Tests.StepDefinitions
{
    [Binding]
    public class CommonSteps
    {
        private static CassiniDevServer _server;
        protected const string URL = "http://localhost:8000";

        [BeforeFeature]
        public static void BeforeFeature()
        {
            _server = new CassiniDevServer();
            _server.StartServer(@"..\..\..\..\Src\DatabaseSchemaReader.Website", 8000, "", "localhost");
        }

        [AfterFeature]
        public static void AfterFeature()
        {
            _server.StopServer();
            WebBrowser.Current.Close();
        }

        [Given("I am on the Home page")]
        public void GivenIAmOnTheHomePage()
        {
            WebBrowser.Current.GoTo(URL);
        }
    }
}