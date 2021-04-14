# MisGastos.Api

Este repo  es para alojar la api que expone los servicios referentes a la gestion de 
gastos registrados

### Alternatively you can also clone the Repository.

1. Clone this Repository and Extract it to a Folder.
3. Change the Connection Strings for the Application and Identity in the MisGastos/appsettings.json - (WebApi Project)
2. Run the following commands on Powershell in the WebApi Projecct's Directory.
- dotnet restore
- dotnet ef database update -Context MisGastosContext
- dotnet run (OR) Run the Solution using Visual Studio 2019

# Dependencies
  Net Core 3.1
  FluentValidation 9.2.2

## Technologies
- ASP.NET Core 3.1 WebApi
- REST Standards
- .NET Core 3.1 / Standard 2.1 Libraries

## Features
- [x] Entity Framework Core - Code First
- [x] Repository Pattern - Generic
- [x] Serilog
- [x] Swagger UI
- [x] Response Wrappers
- [ ] Healthchecks
- [x] Pagination
- [ ] In-Memory Caching
- [ ] Redis Caching
- [x] Microsoft Identity with JWT Authentication
- [ ] Role based Authorization
- [ ] Identity Seeding
- [ ] Database Seeding
- [x] Custom Exception Handling Middlewares
- [ ] Global Exception Filter
- [x] API Versioning
- [x] Fluent Validation
- [x] Automapper
- [ ] SMTP / Mailkit / Sendgrid Email Service
- [ ] Complete User Management Module (Register / Generate Token / Forgot Password / Confirmation Mail)
- [x] User Auditing
- [x] Soft Delete

## Brief Overview
![alt text](https://www.codewithmukesh.com/wp-content/uploads/2020/06/Onion-Architecture-In-ASP.NET-Core.png)

## Prerequisites
- Visual Studio 2019 Community and above
- .NET Core 3.1 SDK and above
- Basic Understanding of Architectures and Clean Code Principles

### Plugins

Dillinger is currently extended with the following plugins. Instructions on how to use them in your own application are linked below.

| Plugin | README |
| ------ | ------ |
| Dropbox | [plugins/dropbox/README.md][PlDb] |
| Github | [plugins/github/README.md][PlGh] |
| Google Drive | [plugins/googledrive/README.md][PlGd] |
| OneDrive | [plugins/onedrive/README.md][PlOd] |
| Medium | [plugins/medium/README.md][PlMe] |
| Google Analytics | [plugins/googleanalytics/README.md][PlGa] |

## About the Author
### Javier Alba
- Twitter - [javialba881](https://www.twitter.com/javialba881)
- Linkedin - [Javier Alba](https://www.linkedin.com/in/javier-albarracin-4a6116160/)

## Licensing
Javier/MisGastos.WebApi Project is licensed with the [MIT License](http://127.0.0.1:3000/ealbarracin/MisGastos.Api/src/master/LICENSE).
