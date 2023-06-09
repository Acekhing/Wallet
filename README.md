### Wallets


Admin login:

           email: admin@hubtel.com
           password: admin
           UserId: 8e445865-a24d-4543-a6c6-9443d048cdb9
           
# WebAPI with ASP.NET Core 3.1
A simple Web API project that handles CRUD operations, authentication, authorization following the CQRS (Command Query Responsibility Segregation) and the Unit of work pattern.
The project is built with ASP.NET Core 3.1 with two databases (MSSQL serving as the authentication server and MongoDB serving as the db server for wallets)

## Features

- Clean architecture: The project follows a modular and layered architecture to ensure separation of concerns and maintainability.
- CQRS pattern: Command Query Responsibility Segregation (CQRS) is implemented to separate read and write operations, improving performance and scalability.
- Repository pattern: The repository pattern is used to abstract data access and provide a consistent interface for working with data.
- API versioning: The project includes API versioning to manage and support different versions of the API.
- Authentication and authorization: The project implements authentication and authorization mechanisms to secure access to API endpoints.

## Prerequisites

- .NET Core 3.1 SDK
- MongoDB

## Getting Started

1. Clone the repository:
   ```shell
   git clone [https://github.com/Acekhing/Wallet.git]
   
2. Build the solution:
```shell
   dotnet build
```

3. Configure the MongoDB and MSSQL connection in the appsettings.json file.
4. Run the migration [package manager console in infrastructure layer]
```
   update-database
```

5. Run the application:
```shell
   dotnet run
```

6. Access the API using your preferred HTTP client.

<br/>
<br/>
<br/>
