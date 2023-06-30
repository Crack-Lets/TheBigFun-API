Feature: EventsServiceTests
As a Developer 
I want to add new Event through API 
In order to make it available for applications.
	
	Background: 
		Given the Endpoint https://localhost:7266/api/v1/events is available 
		
	@event-adding  
	Scenario: Add Event with unique Name 
		When a Post Request is sent 
		  | Name    | Address    | Capacity | Image   | DateTime   | Cost | District  |
		  | Evento1 | Direccion1 | 100      | imagen1 | 2023/03/25 | 25   | Distrito1 |
		Then A Response is received with Status 200 
		And a Event Resource is included in Response Body 
		  | Id |  Name   | Address    | Capacity | Image   | DateTime   | Cost | District  |
		  | 1  | Evento1 | Direccion1 | 100      | imagen1 | 2023/03/25 | 25   | Distrito1 | 
               
	@event-adding 
	Scenario: Add Event with existing Name 
		Given A Event is already stored 
		  | Id |  Name   | Address    | Capacity | Image   | DateTime   | Cost | District  |
		  | 2  | Evento2 | Direccion2 | 70       | imagen2 | 2023/06/27 | 20   | Distrito2 | 
		When a Post Request is sent 
		  | Name    | Address    | Capacity | Image   | DateTime   | Cost | District  |
		  | Evento2 | Direccion2 | 70       | imagen2 | 2023/06/27 | 20   | Distrito2 | 
		Then A Response is received with Status 400 
		And An Error Message is returned with value "Event name already exists."