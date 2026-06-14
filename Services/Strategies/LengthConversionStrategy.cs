using UnitConverterApi.Models;

namespace UnitConverterApi.Services.Strategies;

/// <summary>
/// Handles conversions between units of length. Base unit: meter.
/// </summary>
public class LengthConversionStrategy : FactorBasedConversionStrategy
{
    public override ConversionCategory Category => ConversionCategory.Length;

    protected override IReadOnlyDictionary<string, double> UnitFactors { get; } =
        new Dictionary<string, double>
        {
            ["meter"] = 1.0,
            ["kilometer"] = 1000.0,
            ["centimeter"] = 0.01,
            ["millimeter"] = 0.001,
            ["mile"] = 1609.344,
            ["yard"] = 0.9144,
            ["foot"] = 0.3048,
            ["inch"] = 0.0254,
            ["nauticalmile"] = 1852.0
        };
}
