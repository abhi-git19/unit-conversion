using UnitConverterApi.Models;

namespace UnitConverterApi.Services.Strategies;

/// <summary>
/// Handles conversions between units of volume. Base unit: liter.
/// </summary>
public class VolumeConversionStrategy : FactorBasedConversionStrategy
{
    public override ConversionCategory Category => ConversionCategory.Volume;

    protected override IReadOnlyDictionary<string, double> UnitFactors { get; } =
        new Dictionary<string, double>
        {
            ["liter"] = 1.0,
            ["milliliter"] = 0.001,
            ["cubicmeter"] = 1000.0,
            ["gallon"] = 3.785411784
        };
}
