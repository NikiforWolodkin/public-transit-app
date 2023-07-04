# TransitApplication

This NuGet package provides a set of custom enums, exceptions for use in TransitApplication, as well as middleware for handling these exceptions.

## Installation

To install this package, use the following command in the Package Manager Console:

```
NuGet\Install-Package TransitApplication -Version 1.1.0
```

## Usage 

To use the exception handler, add the following line to your `Program.cs` file, before the `app.Run();` line:

```csharp
using TransitApplication.Extensions;

// ...
app.UseExceptionHandlingMiddleware();
// ...
app.Run();
```

## License

This project is licensed under the MIT License.