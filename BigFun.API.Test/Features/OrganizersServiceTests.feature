Feature: OrganizersServiceTests
As a Developer 
I want to add new Organizer through API 
In order to make it available for applications.
	
	Background: 
		Given the Endpoint https://localhost:7266/api/v1/organizers is available 
		
	@organizer-adding  
	Scenario: Add Organizer with unique Email 
		When a Post Request is sent 
		  | Name     | UserName   | Email               | 
		  | Isabella | Isabella28 | isabella28@gmail.com| 
		Then A Response is received with Status 200 
		And a Organizer Resource is included in Response Body 
		  | Id | Name     | UserName   | Email                |
		  | 1  | Isabella | Isabella28 | isabella28@gmail.com | 
               
	@organizer-adding 
	Scenario: Add Organizer with existing Email 
		Given A Organizer is already stored 
		  | Id | Name   | UserName | Email              |
		  | 1  | Camila | Camila23 | camila23@gmail.com | 
		When a Post Request is sent 
		  |Name   | UserName | Email              |
		  |Camila | Camila23 | camila23@gmail.com | 
		Then A Response is received with Status 400 
		And An Error Message is returned with value "Organizer email already exists."






