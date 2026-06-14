# Unit Converter API

A small .NET 10 Web API that converts numeric values between measurement units (length, weight, temperature, volume, area). The project includes a strategy-based design so adding new categories or units is straightforward.

## Requirements
- .NET 10 SDK
- Visual Studio 2022/2026 (or later) or `dotnet` CLI

## Quick start

Using Visual Studio:
1. Open the solution in Visual Studio.
2. Set `UnitConverterApi` as the startup project in __Solution Explorer__.
3. Select the configuration (e.g., __Debug__) and run (press __F5__).

## Git (repository) guidance

- Repository root: this folder (track solution-level files such as this `README.md`, `.gitignore`, and CI configs).
- Clone:
  git clone <repo-url>
  cd UnitConverterApi\UnitConverterApi
- Branching: use feature branches (`feature/xyz`), short-lived, open pull requests to `main`/`master` (or team default).

## API contract

POST / (controllers are discoverable via Swagger) — see Swagger UI for exact route(s) and examples.

Request body (`ConversionRequest`):
{
  "category": "Length",
  "fromUnit": "meter",
  "toUnit": "foot",
  "value": 1.0
}

Response body (`ConversionResponse`):
{
  "category": "Length",
  "fromUnit": "meter",
  "toUnit": "foot",
  "inputValue": 1.0,
  "convertedValue": 3.28084
}

Error response (consistent JSON shape produced by the middleware):
{
  "status": 404,
  "error": "NotFound",
  "message": "Category 'Foo' is not supported."
}

## Supported categories
- Length
- Weight
- Temperature
- Volume
- Area

Example supported length units (see `Services/Strategies/LengthConversionStrategy.cs` for exact list):
- meter, kilometer, centimeter, millimeter
- mile, yard, foot, inch, nauticalmile

For the exact list of units for other categories, inspect their strategy classes in `Services/Strategies`.

## Extending the API
To add a new conversion category:
1. Implement `IConversionStrategy` (or derive from an existing base like `FactorBasedConversionStrategy`).
2. Register the strategy in `Program.cs` using `AddKeyedScoped<IConversionStrategy, YourStrategy>(ConversionCategory.YourCategory)`.
3. Add the category to the `registeredCategories` array used by the `ConversionStrategyFactory`.

## Notes
- Swagger/OpenAPI is enabled in Development and available under `/swagger`.
- CORS is permissive for demo purposes — tighten for production.
- Centralized error handling is provided by `Middleware/ExceptionHandlingMiddleware.cs`.

## Contributing
Fork, make changes, and open a PR. Keep changes limited to a category/strategy per PR where possible.

## License
This repository does not include a license file. Add one if you plan to share publicly.