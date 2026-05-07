# Courier Management System (ASP.NET Core MVC)

This repository contains a **Clean Architecture** ASP.NET Core MVC application for a Courier Management System, implemented incrementally from the provided documentation.

## Tech Stack

- ASP.NET Core MVC (`net10.0`)
- Entity Framework Core (SQL Server) — **Code First**
- Repository Pattern + Unit of Work
- JWT authentication for API endpoints (minimal starter)
- Docker Compose (SQL Server)

## Solution Structure

`src/`
- `CourierManagement.Domain` — entities/enums (business model)
- `CourierManagement.Application` — service layer + abstractions (interfaces)
- `CourierManagement.Infrastructure` — EF Core `DbContext`, repository implementations, migrations
- `CourierManagement.Web` — MVC UI + minimal JWT-protected API endpoints

## Implemented Features (Phase 1)

- Parcel Type management (CRUD)
- Parcel booking (create order)
  - Auto tracking ID generation
  - Weight-based pricing calculation
  - COD support (adds COD amount to total payable)
- Invoice view (print-friendly)
- Customer tracking portal (by tracking ID)

## Run (LocalDB)

1. Build:
   - `dotnet build src/CourierManagement.Web/CourierManagement.Web.csproj`
2. Run:
   - `dotnet run --project src/CourierManagement.Web`
3. On first run (Development), the app:
   - applies EF migrations automatically
   - seeds a few default `ParcelType` rows

Default connection string is in `src/CourierManagement.Web/appsettings.json`.

## Run SQL Server via Docker

1. Copy `.env.example` to `.env` and set a strong password.
2. Start SQL Server:
   - `docker compose up -d`
3. Run the web app with a Docker SQL connection string override:
   - PowerShell:
     - `$env:ConnectionStrings__DefaultConnection = "Server=localhost,1433;Database=CourierManagementDb;User Id=sa;Password=YourStrong!Passw0rd;TrustServerCertificate=True;MultipleActiveResultSets=true"`
     - `dotnet run --project src/CourierManagement.Web`

## JWT API (Starter)

- Get token:
  - `POST /api/auth/token`
  - Body: `{ "username": "api", "password": "api123!" }`
- Track parcel (JWT required):
  - `GET /api/tracking/{trackingId}`
  - Header: `Authorization: Bearer <token>`

Credentials and JWT settings live in `src/CourierManagement.Web/appsettings.json` under `ApiAuth` and `Jwt`.

## Notes for This Environment

The repo includes a few MSBuild workarounds in `Directory.Build.props` to make builds reliable in restricted environments.
