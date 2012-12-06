Feature: Login

Scenario: Logging in with valid credentials
	Given I am on the Home page	
	Given I click the 'signInLink' link
	When I fill in the following form
	| field    | value    |
	| Email | testuser@test.com |
	| Password | testpass |
	And I click the 'ButtonRegister' button
	Then I should be at the 'DatabaseExplorer' page

