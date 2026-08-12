# AccountingSystem

A simple journal entry API built with a clean **n-tier architecture** using ASP.NET Core (.NET 10), Entity Framework Core, and PostgreSQL.

## Architecture

The project follows an n-tier (layered) architecture with three layers:

```
Accounting.App      -> Presentation layer (ASP.NET Core Web API)
Accounting.Service  -> Business logic layer (services, DTOs, validators, exceptions)
Accounting.Dao      -> Data access layer (EF Core DbContext, entities, repositories)
```

```
┌─────────────────┐
│   Accounting.App │  Controllers, Middlewares, Program.cs
└────────┬────────┘
         │
┌────────▼─────────┐
│ Accounting.Service│  Business logic & validation
└────────┬─────────┘
         │
┌────────▼─────────┐
│  Accounting.Dao  │  EF Core + PostgreSQL
└─────────────────┘
```

### Layers

| Layer | Project | Responsibility |
|-------|---------|----------------|
| Presentation | `Accounting.App` | HTTP controllers, exception handling, Serilog logging, OpenAPI/Scalar UI |
| Business | `Accounting.Service` | Journal entry business rules, FluentValidation, DTOs, custom exceptions |
| Data | `Accounting.Dao` | Entity models, EF Core `AppDbContext`, repository interfaces/implementations, EF migrations |

## Features

- Create journal entries with multiple lines (debit/credit)
- Business rule enforcement:
  - Balanced journal entry (total debits == total credits)
  - Duplicate voucher number detection
  - Account existence validation
- Soft-delete journal entries
- Global exception handling returning RFC 7807 Problem Details
- Structured logging with Serilog (console + daily rolling file)
- FluentValidation request validators
- OpenAPI documentation with Scalar UI
- EF Core migrations with snake_case naming convention

## Tech Stack

- .NET 10 / ASP.NET Core Web API
- Entity Framework Core 10 (PostgreSQL via Npgsql)
- FluentValidation
- Serilog
- Scalar (OpenAPI UI)
- PostgreSQL

## Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- PostgreSQL

### Setup

1. Clone the repository:

   ```bash
   git clone https://github.com/<your-username>/AccountingSystem.git
   cd AccountingSystem
   ```

2. Configure the connection string in `Accounting.App/appsettings.json`:

   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=127.0.0.1;port=5432;Database=AccountingDb;Username=postgres;Password=yourpassword"
   }
   ```

3. Apply the database migrations:

   ```bash
   dotnet ef database update --project Accounting.Dao --startup-project Accounting.App
   ```

4. Run the application:

   ```bash
   dotnet run --project Accounting.App
   ```

5. Open the Scalar API documentation at `https://localhost:<port>/scalar/v1`.

## API Endpoints

### Create a Journal Entry

`POST /api/journal-entries`

Request body:

```json
{
  "voucherNo": "JV-001",
  "transactionDate": "2026-08-12T00:00:00Z",
  "description": "Office supplies purchase",
  "lines": [
    {
      "accountId": "00000000-0000-0000-0000-000000000001",
      "debitAmount": 1000.00,
      "creditAmount": 0
    },
    {
      "accountId": "00000000-0000-0000-0000-000000000002",
      "debitAmount": 0,
      "creditAmount": 1000.00
    }
  ]
}
```

Responses: `201 Created` on success, `400 Bad Request` on validation failure.

### Delete a Journal Entry

`DELETE /api/journal-entries/{id}`

Soft-deletes a journal entry. Responses: `204 No Content` on success, `404 Not Found` if the entry does not exist.

## Project Structure

```
AccountingSystem/
├── Accounting.App/            # Presentation layer
│   ├── Controllers/           # JournalEntryController
│   ├── Extensions/            # DI registration
│   ├── Middlewares/           # GlobalExceptionHandler
│   └── Program.cs
├── Accounting.Service/        # Business layer
│   ├── DTOs/                  # Requests & responses
│   ├── Exceptions/            # BadRequestException, NotFoundException
│   ├── Interfaces/            # IJournalEntryService
│   ├── Implementations/       # JournalEntryService
│   └── Validators/            # FluentValidation validators
├── Accounting.Dao/            # Data access layer
│   ├── Context/               # AppDbContext
│   ├── Entities/              # Account, JournalEntry, JournalEntryLine
│   ├── EntityConfigurations/  # EF Core configurations
│   ├── Interfaces/            # Repository interfaces
│   ├── Implementations/       # Repository implementations
│   └── Migrations/            # EF Core migrations
└── AccountingSystem.slnx
```

## Contributing

Contributions are welcome. Please open an issue or submit a pull request.

## License

[MIT](LICENSE)
