Feature: Register

Scenario: Register in with valid credentials
	Given I am on the Home page	
	Given I click the 'registerLink' link
	When I fill in the following form
	| field    | value    |
	| RegisterEmail | testuser@test.com |
	| RegisterPassword | testpass |
	| RegisterConfirmationPassword | testpass |
	And I click the 'ButtonRegister' button
	Then I should be at the 'DatabaseExplorer' page
