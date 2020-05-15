# Simple-AspNetCore-Web-API-XUnit
A Simple AspNetCore Web API using basic Auth and XUnit 

General API Description
1. Get api/cars
	URI Parameters : None
	Auth : Basic Authentication
	Body Parameters : None
	
2. Get api/cars/{id}
	URI Parameters : id,	 Type:integer,	Required
	Auth : Basic Authentication
	Body Parameters : None
	
3. Post api/cars
	URI Parameters : None
	Auth : Basic Authentication
	Content Type : Application/Json
	Body Parameters : Json string of Car Object
	Sample	: {"id": 1001,"make": "Alfa Romeo 2",
	        "model": "Giulia","year": 2018,
	        "countryManufactured": "Italy",
	        "colour": "White","price": 50000}
    
4. Put api/cars
	URI Parameters : None
	Auth : Basic Authentication
	Content Type : Application/Json
	Body Parameters : Json string
	Body Parameters : Json string of Car Object
	Sample	: {"id": 1001,"make": "Alfa Romeo 2",
	        "model": "Giulia","year": 2018,
	        "countryManufactured": "Italy",
	        "colour": "White","price": 50000}
	
5. Post api/cars/discount
	URI Parameters : None
	Auth : Basic Authentication
	Content Type : Application/Json
	Body Parameters : Json string of List<int> represents Car property Id
	Sample	: [1,2,3]

Postmant Basic Auth:
(Simply check at AspNetCore.Cars.API\Services\UserService.cs)
	{Username : test
	Password : test},
	{Username : admin
	Password : admin}
