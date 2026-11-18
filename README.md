# TaskManager

Cohort 10 — .NET Fullstack (.NET+ReactJS) assignment for Taha Akhtar.

Task management app built with ASP.NET Core (.NET 10, Clean Architecture) and React.

## Structure

```
src/
  TaskManager.Api             # Web API, middleware, configuration
  TaskManager.Application     # Use cases, interfaces, DTOs
  TaskManager.Domain          # Entities, domain exceptions
  TaskManager.Infrastructure  # EF Core, SQL Server, external services
tests/
  TaskManager.Tests           # xUnit tests
```

Dependencies: Api → Application, Infrastructure · Infrastructure → Application, Domain · Application → Domain

## Prerequisites

- .NET 10 SDK
- SQL Server (local instance or Docker)

## Setup

```bash
dotnet restore
dotnet build
```

Set the connection string in `src/TaskManager.Api/appsettings.Development.json`, or keep it out of source control with user secrets:

```bash
dotnet user-secrets init --project src/TaskManager.Api
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "<your-connection-string>" --project src/TaskManager.Api
```

## Run

```bash
dotnet run --project src/TaskManager.Api
```

The OpenAPI document is served at `/openapi/v1.json` in Development.

## Test

```bash
dotnet test
```
