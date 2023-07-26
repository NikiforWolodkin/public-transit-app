# TransitApplication

This NuGet package provides a set of custom enums, messaging contracts and exceptions for use in TransitApplication, as well as middleware for handling these exceptions.

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