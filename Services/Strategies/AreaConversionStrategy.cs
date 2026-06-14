using UnitConverterApi.Models;

namespace UnitConverterApi.Services.Strategies;

/// <summary>
/// Handles conversions between units of area. Base unit: square meter.
/// </summary>
public class AreaConversionStrategy : FactorBasedConversionStrategy
{
    public override ConversionCategory Category => ConversionCategory.Area;

    protected override IReadOnlyDictionary<string, double> UnitFactors { get; } =
        new Dictionary<string, double>
        {
            ["squaremeter"] = 1.0,
            ["squarekilometer"] = 1_000_000.0,
            ["squarecentimeter"] = 0.0001,
            ["squaremile"] = 2_589_988.110336,
            ["squareyard"] = 0.83612736,
            ["squarefoot"] = 0.09290304,
            ["acre"] = 4046.8564224,
            ["hectare"] = 10000.0
        };
}
