## Order Management API

## Overview
A fullstack-ready ASP.NET Core Web API for managing Customers, Products, and Orders.  
Includes DTOs, AutoMapper for clean mapping, and xUnit tests for reliability.


## Tech Stack
- **ASP.NET Core 8** (Web API)
- **Entity Framework Core** (InMemory + SQL Server ready)
- **AutoMapper** (DTO ↔ Entity mapping)
- **xUnit + Moq** (unit testing)
- **OpenAPI** (API documentation)



### Features
- CRUD endpoints for Customers, Products, and Orders
- DTOs for clean API contracts
- AutoMapper integration for mapping
- Global error handling middleware
- Unit tests for controllers


## Testing
Unit tests are implemented with **xUnit**, **Moq**, and **EF Core InMemory**.

### Run Tests
```bash
dotnet test



### Getting Started
1. Clone the repo
2. Run migrations: `dotnet ef database update`
3. Launch the API: `dotnet run`
4. Test endpoints via `OrderManagementAPI.http`