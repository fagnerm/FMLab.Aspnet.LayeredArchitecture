## Project Overview

ASP.NET Core 8 boilerplate implementing Layered Architecture.

## Stack

- **API**: ASP.NET Core 8 Minimal API
- **ORM**: Entity Framework Core 8
- **Database**: Postgres 16
- **Reverse proxy**: Nginx (2 API instances, keepalive)

## Architecture
**Dependency direction:**
> Business -> Infrastrucure -> Api

### Layers

- **Api**: Routes, auth, request/response mapping.
- **Business**: Services, value objects, DTOs, interfaces.
- **Infrastructure**: EF Core, repositories, external services.

### Key Patterns

- **Result Pattern**: Common return type for use case outcomes, replacing exception-based control flow.
- **IRepository + IQuery**: Separates transaction operations(repository) from read operations(query).
- **Value Objects**: Immutable types that encapsulate and validate their own business rules.

## API Endpoints

- `POST   /users` - Register a new user
- `GET    /users` - List users
- `GET    /users/{id}` - Get a user by ID
- `PUT    /users/{id}` - Full update. Unset fields are cleared
- `PATCH  /users/{id}` - Partial update. Only provided fields are changed
- `DELETE /users/{id}` - Remove a user
- `POST   /users/{id}/deactivate` - Deactivate a user

## License

This project is licensed under the [MIT License](LICENSE).
