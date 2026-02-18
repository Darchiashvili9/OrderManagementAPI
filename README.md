## Order Management API

This project implements a baseline CRUD API for managing Customers, Products, and Orders.

### Features
- **Customers**: Create, read, update, delete
- **Products**: Create, read, update, delete
- **Orders**: Create, read, update, delete, with linked Customer and OrderItems
- **DTOs**: All controllers return DTOs to ensure clean API responses and avoid serialization cycles
- **Documentation**: Request examples for all endpoints are included in `OrderManagementAPI.http`

### Getting Started
1. Clone the repo
2. Run migrations: `dotnet ef database update`
3. Launch the API: `dotnet run`
4. Test endpoints via `OrderManagementAPI.http`