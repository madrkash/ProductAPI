# ProductStore API

A simple CRUD API that deals with Products and Product Options as the core entities.

Please find below the details of different aspects of development that I have attempted to bring in to the shared solution.


## How to See It in Action

Clone the repo

Navigate to path `~\ProductAPI\` and run the command `docker-compose up`

Swagger will then be accessible at  [http://localhost:8000/swagger/index.html](http://localhost:8000/swagger/index.html)

Alternatively, after docker-compose is run, we can open the solution from Visual Studio using the launch profile **ProductStore.API**
 
and the swagger link would then be [http://localhost:5000/swagger/index.html](http://localhost:5000/swagger/index.html)


## Core Technologies

1. Framework: ASP.NET Core MVC 3.1
2. Language: C# 
3. Database: PostgreSQL

## Refactoring

### Architecture

I chose to use  **Clean / Onion Architecture** as it makes the solution independent of 
1. Framework & anything external
2. Client consuming it 
3. Infrastructure (Database in this context) 
4. Easily testable due to loose coupling

### Engineering Principles

I have based my solution on SOLID principles by employing: 

1. Constructor based **dependency injection** to allow for inversion of control.
2. Responsibility delegation wherever applicable.
3. Small and appropriate interfaces ensuring substitution and ease of testing.
4. Generics where appropriate to support code reusability.
5. Asynchronous flow throughout the application.
6. **Automapper** to separate out the Api models from the database ones.

### Diagnostics

1. Included appropriate exception handling both at global and specific scenario levels.
2. Implemented logging using  **Serilog** and **Serilog File Sink**.
3. Returned the appropriate status codes for different find of failures.

### Resilience
1. Input validation using **FluentValidation**.

### Database
1. Used **Dapper** as the ORM.
2. Used **Dapper.SimpleCRUD** to handle the basic CRUD operations as it helps keep the code generic and eliminates the need for repeated DAL code.
3. Used **DbUp** for database migration including creation and default data population at startup.   
4. Constrained the database with entity relationship and ensured integrity via the API by implementing Cascade Delete.

### Unit Testing
1. Used **xUnit** as the Unit testing library along with Moq.
2. Employed Object builder pattern for keeping unit tests clean and simple.
3. Employed Xunit Data Driven testing where applicable with both *InlineData* and *ClassData*.
4. Tested all the individual components with specific responsibilities separately. 

### Integration Testing
1. Comprehensive controller level testing covering all different success and failure CRUD scenarios by interacting with a separate test database running inside another container.
2. Integration tests can be run after navigating to the path `~\ProductAPI\` by running the command 
`docker-compose -f docker-compose.tests.yml up`.

### Containerization using docker-compose
1. Container communication between API and DB.
2. A separate docker-compose setup for container communication between integration tests project and its own test DB.
### Documentation
1. **Swagger** 

### Assumptions 
1. Delivery Price is also mandatory.
2. Authentication is not mandatory.

## TODO

If I had more time, I would have liked to implement the below features:

1. Implement Jwt Authentication and corresponding Authorization to secure the APIs.

2. Better and comprehensive logging at different levels.

3. Host the solution on AWS infrastructure
