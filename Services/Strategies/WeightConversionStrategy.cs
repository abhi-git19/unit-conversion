using UnitConverterApi.Models;

namespace UnitConverterApi.Services.Strategies;

/// <summary>
/// Handles conversions between units of weight/mass. Base unit: kilogram.
/// </summary>
public class WeightConversionStrategy : FactorBasedConversionStrategy
{
    public override ConversionCategory Category => ConversionCategory.Weight;

    protected override IReadOnlyDictionary<string, double> UnitFactors { get; } =
        new Dictionary<string, double>
        {
            ["kilogram"] = 1.0,
            ["gram"] = 0.001,
            ["milligram"] = 0.000001,
            ["metricton"] = 1000.0,
            ["pound"] = 0.45359237,
            ["ounce"] = 0.028349523125,
        };
}
