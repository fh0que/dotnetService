# OrderAPI

A minimal .NET 10 Web API for managing orders, built with a lightweight CQRS pattern and EF Core (SQLite).

## Stack

- .NET 10 minimal APIs
- Entity Framework Core + SQLite
- FluentValidation

## Project structure

- `Commands/` — write-side command records (e.g. `CreateOrderCommand`)
- `Queries/` — read-side query records (e.g. `GetOrderByIdQuery`)
- `Handlers/` — `ICommandHandler<TCommand, TResult>` / `IQueryHandler<TQuery, TResult>` implementations
- `Dtos/` — response DTOs
- `Models/` — EF Core entities
- `Data/` — `AppDbContext`
- `Migrations/` — EF Core migrations

## Endpoints

- `POST /api/orders` — create an order
- `GET /api/orders/{id}` — fetch an order by id

## Running locally

```bash
dotnet restore
dotnet ef database update
dotnet run
```
