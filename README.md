# RoomHub

Oprea Marian Alexandru, 3E2, .NET midterm.

Study room bookings API, built with vertical slices, a rich domain, `Result<T>`, command and query handlers,
and EF Core + SQLite behind Repository and Unit of Work.

## Run

```bash
dotnet run --project src/RoomHub.Api
```

The application starts on `http://localhost:5090`. The database file `roomhub.db` is created on first start.
The test requests are in `roomhub.http`.

## Structure

- `src/RoomHub.Api/Domain`: entities, value objects, errors and repository contracts
- `src/RoomHub.Api/Features`: one folder per use case (command or query, handler, endpoint)
- `src/RoomHub.Api/Infrastructure/Persistence`: DbContext, EF Core configurations and repositories
- `src/RoomHub.Api/Common`: CQRS contracts, handler registration and error to HTTP mapping
