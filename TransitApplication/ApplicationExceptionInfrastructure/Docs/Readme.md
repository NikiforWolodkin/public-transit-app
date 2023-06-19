# TransitApplication

This NuGet package provides a set of custom exceptions for use in TransitApplication, as well as middleware for handling these exceptions.

## Installation

To install this package, use the following command in the Package Manager Console:

```
Install-Package TransitApplication
```

To use the custom exceptions, simply add a reference to the `TransitApplication.Exceptions` namespace and throw the appropriate exception when needed.

To use this package, add the following line to your `Program.cs` file, before the `app.Run();` line:

```csharp
using TransitApplication.Extensions;

// ...
app.UseExceptionHandlingMiddleware();
// ...
app.Run();
```

## License

This project is licensed under the MIT License. See the LICENSE file for more information.