# sample-incident-api
Sample project for Incident REST API

## To build and run using VS 2017 or later
1. Load `IncidentApi.sln` into the IDE
1. Build the solution
1. Optionally add more data files to `src/Sample.Incident.WebApi/bin/Debug/netcoreapp2.2/data`
1. Set `Sample.Incident.WebApi` as a startup project
1. Press F5

## To run integration tests in VS
1. Load `IntegrationTests.sln` into the IDE
1. Build the solution
1. Make sure the API is running as described above
1. In the menu select `Test -> Run -> All Tests`

## Potential improvements
1. Add logging
1. Add Open API specifications and Swagger UI
1. Possible retries for weather API
1. Remove hard-coded secret
1. Compose docker image
1. Better test coverage
1. Complete incident object model
1. Service independent weather model
