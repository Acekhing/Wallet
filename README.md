### Wallets


Admin login:

           email: admin@hubtel.com
           password: admin
           UserId: 8e445865-a24d-4543-a6c6-9443d048cdb9
           
           
# WebAPI with ASP.NET Core 3.1
A simple Web API project that handles CRUD operations, authentication, authorization and caching following the CQRS (Command Query Responsibility Segregation) and the Unit of work pattern.

The project is built with ASP.NET Core 3.1 with two databases (MSSQL serving as the authentication server and MongoDB serving as the database for wallets). Also in the project, Redis is used for caching some part of data etc.

All features are listed below:

## Features

- Caching: Caching is very important for readily accessed data. Here, Redis is used for caching wallet list.
- Logging: The project uses serilog for logging and sinks into Elastic search and visualize using kibana
- Clean architecture: The project follows a modular and layered architecture to ensure separation of concerns and maintainability.
- CQRS pattern: Command Query Responsibility Segregation (CQRS) is implemented to separate read and write operations, improving performance and scalability.
- Repository pattern: The repository pattern is used to abstract data access and provide a consistent interface for working with data.
- API versioning: The project includes API versioning to manage and support different versions of the API.
- Authentication and authorization: The project implements authentication and authorization mechanisms to secure access to API endpoints.


## Prerequisites

- .NET Core 3.1 SDK
- Docker desktop
- Redis server

## Required Docker images
- Elastic search 8.7.1
- kibana 8.7.1
- Mongo latest
- Mongo-express latest

## Access points
- Elastic search ```[http://localhost:9200]```
- kibana ```[http://localhost:5601]```
- Mongo db ```[http://localhost:27017]```
- Mongo express ```[http://localhost:8081]```

## Getting Started

1. Clone the repository:
```shell
   git clone [https://github.com/Acekhing/Wallet.git]
```
 
2. Run docker compose
```shell
   docker-compose up -d
```
   
3. Build the solution:
```shell
   dotnet build
```

4. Configure the MSSQL and Redis connection in the appsettings.json file

5. Run the migration [package manager console in infrastructure layer]
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

### Account Scheme
![account-scheme](https://github.com/Acekhing/Wallet/assets/65813017/c0acd850-8c73-432b-83bb-4fc05eb3ae63)
### Wallet type
![wallet-type](https://github.com/Acekhing/Wallet/assets/65813017/1689f4e2-2de8-47c5-bf30-f205bd652011)
### Wallet
![wallet](https://github.com/Acekhing/Wallet/assets/65813017/c6742376-af72-446e-add4-7b1fc8987da3)
