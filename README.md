# User Location API

This ASP.NET REST API can be used to store data detailing the geographic whereabouts of users over time. It provides an OpenAPI-conforming spec in `swagger.json`, and can be interacted with via an auto-generated Swagger UI.

## Setup Instructions

1. Open `AirboxTechnical.sln` in Visual Studio 2022.
2. Debug the solution via the default configuration - a Swagger UI should open in your browser at `https://localhost:7055`. If it does not, you might need to run `dotnet dev-certs https --trust` and restart your browser.
3. Use the Swagger UI to interact with the various API endpoints - start by creating a user, then create some user locations.
