Feature: LoginPageSpecflow
	In order to Login
	I need to fill the form

@loginTestUnique
Scenario: Succesfully Login with unique name and email
	Given User initialize Driver
	And I Navigate to login page
	When I fill the form with credentials
	| Name | Email   | Password   |
	| test | test@gmail.com | test12345! |
	And press login
	Then the login should be ok
	And I should close driver

@loginTestDuplicated
Scenario: Unsuccesfully Login with repeated name and email
	Given User initialize Driver
	And I Navigate to login page
	When I fill the form with credentials that already exists
	| Name | Email   | Password   |
	| test456| test123456@gmail.com | test12345! |
	| test456 | test123456@gmail.com | test12345! |
	Then the login should not be ok
	And I should close driver