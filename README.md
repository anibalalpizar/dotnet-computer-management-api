# Computer Management API

A comprehensive RESTful API for managing computers and their installed software, built with .NET 8 and following Clean Architecture principles.

## Features
- ✅ .NET 8 Web API
- ✅ Clean Architecture with Repository Pattern
- ✅ Swagger/OpenAPI Documentation
- ✅ Entity Framework Core with In-Memory Database
- ✅ Comprehensive CRUD Operations
- ✅ FluentValidation for Input Validation
- ✅ AutoMapper for Object Mapping

## Folder Structure:
```
ComputerApi/                          # 📁 Root folder
├── ComputerApi.sln                   # 📄 Solution file
├── ComputerApi.Domain/               # 📁 Domain Project
│   ├── ComputerApi.Domain.csproj     # 📄 Project file
│   ├── Entities/
│   ├── Interfaces/
│   └── Enums/
├── ComputerApi.Application/          # 📁 Application Project
│   ├── ComputerApi.Application.csproj # 📄 Project file
│   ├── DTOs/
│   ├── Services/
│   └── Validators/
├── ComputerApi.Infrastructure/       # 📁 Infrastructure Project
│   ├── ComputerApi.Infrastructure.csproj # 📄 Project file
│   ├── Data/
│   └── Repositories/
└── ComputerApi.API/                  # 📁 API Project
    ├── ComputerApi.API.csproj        # 📄 Project file
    ├── Controllers/
    ├── Program.cs
    └── appsettings.json
```
## Technologies Used
- .NET 8
- Entity Framework Core
- Swagger/OpenAPI
- AutoMapper
- FluentValidation
