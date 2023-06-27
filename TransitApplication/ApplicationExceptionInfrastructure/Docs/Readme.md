# TransitApplication

This NuGet package provides a set of custom exceptions for use in TransitApplication, as well as middleware for handling these exceptions.

## Installation

To install this package, use the following command in the Package Manager Console:

```
NuGet\Install-Package TransitApplication -Version 1.0.3
```

To use the custom exceptions, simply add a reference to the `TransitApplication.HttpExceptions` namespace and throw the appropriate exception when needed.

To use this package, add the following line to your `Program.cs` file, before the `app.Run();` line:

```csharp
using TransitApplication.Extensions;

// ...
app.UseExceptionHandlingMiddleware();
// ...
app.Run();
```

## License

This project is licensed under the MIT License.