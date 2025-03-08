# MoneyCheck

An ASP.NET Core web API for a budget web application managing categories, units, currencies, versions, and years with budget follow-up etc.
It utilizes Clean Architecture with messaging service and the main purpose of the project is to develop .NET skills and to adhere to best practices.

### Notes
- Uses ASP.NET Core, .NET9.0
- Fluent validation is used for validating input data
- MediatR is used for managing messages
- Serilog is used for logging
- Custom error handling
- Data is stored in MySQL but could easily be changed to any other database

### TODO's
- Unit and integration tests
- Add more validations

### Prerequisites

- Latest edition of [Visual Studio](https://visualstudio.microsoft.com/).
- [Git](https://git-scm.com/)

### Installing

1. [Clone repo](https://docs.github.com/en/repositories/creating-and-managing-repositories/cloning-a-repository) (github docs) or use [Visual Studio](https://learn.microsoft.com/en-us/visualstudio/version-control/git-clone-repository?view=vs-2022)
2. Database setup if using [MySQL](https://www.mysql.com/products/workbench/)
3. [EF Core Migration](https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli)

## Running unit tests

Tests are planned under TODOs

## Built With

* [Visual Studio 2022](https://visualstudio.microsoft.com/) - The IDE
* ASP.NET Core and library templates

## Packages Used

- BCrypt.Net-Next - Authorization
- MediatR - Message handling
- Microsoft.EntityFrameworkCore.Tools - Migrations and DbContext
- Microsoft.Extensions.Options.ConfigurationExtensions - Option configuration
- Microsoft.Extensions.Configuration.Json - Json configuration
- Microsoft.AspNetCore.SpaServices.Extensions - SPA application helper
- Microsoft.AspNetCore.Authentication.JwtBearer - Authentication middleware
- Serilog - Logging
    .AspNetCore
    .ExtensionsLogging
    .Settings.Configuration
    .Sinks.File
- Swashbuckle - Swagger tools
    .AspNetCore
    .AspNetCore.Swagger
- FluentValidation - Validation
    .DependencyInjectionExtensions
- Pomelo.EntityFrameworkCore.MySql - MySQL database provider

## Versioning

For the versions available, see the [tags on this repository](https://github.com/larsn-cndesign/money-check-backend/tags).

## Authors

**Lars Norrstrand**

- [CN Design AB](https://www.cndesign.se/)
- [Email](mailto:lars.norrstrand@cndesign.se)

## License

This project is licensed under the MIT License.