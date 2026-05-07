# Courier Management System (ASP.NET Core MVC)

This repository contains a **Clean Architecture** ASP.NET Core MVC application for a Courier Management System.

## What It Does (Short)

Create and manage courier shipments end-to-end: configure parcel types, book parcels (automatic tracking ID + price calculation), generate invoices, and track shipments via a customer portal or a small JWT-protected API.

## Implemented Features (Phase 1)

- Parcel Type management (CRUD)
- Parcel booking (create order)
  - Auto tracking ID generation
  - Weight-based pricing calculation
  - COD support (adds COD amount to total payable)
- Invoice view (print-friendly)
- Customer tracking portal (by tracking ID)

## Core Business Logic

- **Tracking ID generation:** every booking produces a unique tracking ID used across invoice and tracking.
- **Pricing:** total payable is computed from parcel type and weight (weight-based pricing rules).
- **COD (Cash on Delivery):** when enabled, COD amount is included in the total payable and shown on the invoice.
- **Consistency:** repositories + Unit of Work coordinate writes; in Development, migrations/seed run automatically.

## Typical Workflow

1. **Set up parcel types** (admin): create/update `ParcelType` records (pricing inputs used during booking).
2. **Book a parcel** (staff): enter sender/receiver + parcel details → system generates tracking ID and computes charges.
3. **Print invoice** (staff/customer): open the invoice view for the booking (print-friendly).
4. **Track shipment** (customer/API): use tracking ID in the portal, or call `GET /api/tracking/{trackingId}` with JWT.

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
