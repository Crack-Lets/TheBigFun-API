Feature: BigFunFrontEndWithBigFunBackEndTests
As a visitor
I want to be able to log in to the application
So I can enjoy all the benefits of it

    Background:
        Given that the BigFunBackEnd is running
        And the BigFunFrontEnd is running

    Scenario: Visitor logs in to the application
        Given the visitor enters his username 
          | Name     | Password   | 
          | Isabella | Isabella28 | 
        When the user clicks the login button
        Then the visitor is logged in to the application
        And a valid user session is established

    Scenario: The visitor tries to log in with incorrect credentials
        Given the visitor enters his username 
          | Name  | Password  | 
          | raulV | ipassword | 
        When the user clicks the login button
        Then an error message is displayed on the frontend
        And the user session is not established