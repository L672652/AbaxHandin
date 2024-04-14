# AbaxHandin
 
This solution uses SQLite as it is easier to use than a real database, i understand this is not ideal.  
This solution also uses a directory that you need for the database to be shared between the service and API, the directory is c:/DatabaseFolder which is not created by the application.  
It is coded in C# dotnet 8.0 and consists of two programs the api and the service that collects the data.  
The api has two points one called bus which gets all the buses available, the other is called closebuses which takes two query parameters called Lat and Long which are for your current position  

Documentation for the MQTT definiton i found here:  
https://www.digitransit.fi/en/developers/apis/4-realtime-api/vehicle-positions/high-frequency-positioning/  
https://digitransit.fi/en/developers/apis/4-realtime-api/vehicle-positions/digitransit-mqtt/

