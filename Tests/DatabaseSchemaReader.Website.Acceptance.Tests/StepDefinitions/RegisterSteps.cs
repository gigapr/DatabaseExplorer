using CassiniDev;
using DatabaseSchemaReader.Website.Acceptance.Tests.StepHelpers;
using NUnit.Framework;
using TechTalk.SpecFlow;
using WatiN.Core;
using Table = TechTalk.SpecFlow.Table;

namespace DatabaseSchemaReader.Website.Acceptance.Tests.StepDefinitions
{
    [Binding]
    public class RegisterSteps : CommonSteps
    {
        [Given(@"I navigate to the '(.*)' page")]
        public void GivenINavigateToThePage(string p0)
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"I click the '(.*)' link")]
        public void GivenIClickTheButton(string p0)
        {
            var link = WebBrowser.Current.Link(Find.ById(p0));

            if (link.Exists)
            {
                link.Click();
            }
            else
            {
                Assert.Fail("Expected to find a link with the value of '{0}'.", p0);
            }
        }

        [When("I click the '(.*)' button")]
        public void AndIClickAButton(string buttonText)
        {
            var button = WebBrowser.Current.Button(Find.ById(buttonText));

            if (button.Exists)
            {
                button.Click();
            }
            else
            {
                Assert.Fail("Expected to find a button with the value of '{0}'.", buttonText);
            }
        }

        [When("I fill in the following form")]
        public void WhenIFillInTheFollowingForm(Table table)
        {
            foreach (var row in table.Rows)
            {
                var textField = WebBrowser.Current.TextField(Find.ById(row["field"]));

                if (textField.Exists)
                {
                    textField.TypeText(row["value"]);
                }
                else
                {
                    Assert.Fail("Expected to find a text field with the name of '{0}'.", row["field"]);
                }
            }
        }

        [Then("I should be at the '(.*)' page")]
        public void ThenIShouldBeAtTheHomePage(string pageName)
        {
            var expectedURL = string.Format("{0}/{1}", URL, pageName);
            var actualURL = WebBrowser.Current.Url;
            Assert.AreEqual(expectedURL, actualURL);
        }

        [Then(@"I should see a validation message")]
        public void ThenIShouldSeeAValidationMessage()
        {
            ScenarioContext.Current.Pending();
        }

    }
}