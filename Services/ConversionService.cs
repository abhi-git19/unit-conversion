using UnitConverterApi.Models;

namespace UnitConverterApi.Services;

public class ConversionService(IConversionStrategyFactory factory, ILogger<ConversionService> logger) : IConversionService
{
    private readonly IConversionStrategyFactory _factory = factory;
    private readonly ILogger<ConversionService> _logger = logger;

    public ConversionResponse Convert(ConversionRequest request)
    {
        var strategy = _factory.GetStrategy(request.Category);

        var result = strategy.Convert(request.FromUnit, request.ToUnit, request.Value);

        _logger.LogInformation(
            "Converted {Value} {FromUnit} to {Result} {ToUnit} for category {Category}",
            request.Value, request.FromUnit, result, request.ToUnit, request.Category);

        return new ConversionResponse
        {
            Category = request.Category,
            FromUnit = request.FromUnit,
            ToUnit = request.ToUnit,
            InputValue = request.Value,
            ConvertedValue = result
        };
    }

    public SupportedUnitsResponse GetSupportedUnits(ConversionCategory category)
    {
        var strategy = _factory.GetStrategy(category);
        return new SupportedUnitsResponse
        {
            Category = category,
            Units = strategy.SupportedUnits.OrderBy(u => u)
        };
    }

    public IEnumerable<SupportedUnitsResponse> GetAllSupportedUnits()
    {
        return _factory.GetAllStrategies()
            .Select(s => new SupportedUnitsResponse
            {
                Category = s.Category,
                Units = s.SupportedUnits.OrderBy(u => u)
            })
            .OrderBy(r => r.Category.ToString());
    }
}
