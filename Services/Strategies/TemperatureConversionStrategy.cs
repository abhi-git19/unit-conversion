using UnitConverterApi.Exceptions;
using UnitConverterApi.Models;

namespace UnitConverterApi.Services.Strategies;

/// <summary>
/// Handles conversions between Celsius, Fahrenheit, and Kelvin.
/// Temperature is not a simple multiplicative scale (it has offsets), so it
/// cannot reuse <see cref="FactorBasedConversionStrategy"/> and instead
/// converts everything via Kelvin as the common base.
/// </summary>
public class TemperatureConversionStrategy : IConversionStrategy
{
    private const double AbsoluteZeroKelvin = 0.0;

    private static readonly HashSet<string> Units = new(StringComparer.OrdinalIgnoreCase)
    {
        "celsius", "fahrenheit", "kelvin"
    };

    public ConversionCategory Category => ConversionCategory.Temperature;

    public IReadOnlyCollection<string> SupportedUnits => [.. Units];

    public double Convert(string fromUnit, string toUnit, double value)
    {
        var from = Normalize(fromUnit);
        var to = Normalize(toUnit);

        var kelvin = ToKelvin(from, value);

        if (kelvin < AbsoluteZeroKelvin)
        {
            throw new InvalidConversionValueException(
                $"Value {value} {from} is below absolute zero, which is not physically valid.");
        }

        return FromKelvin(to, kelvin);
    }

    private string Normalize(string unit)
    {
        var normalized = unit.Trim().ToLowerInvariant();
        if (!Units.Contains(normalized))
        {
            throw new UnsupportedUnitException(unit, Category.ToString());
        }
        return normalized;
    }

    private static double ToKelvin(string unit, double value) => unit switch
    {
        "celsius" => value + 273.15,
        "fahrenheit" => (value - 32) * 5.0 / 9.0 + 273.15,
        "kelvin" => value,
        _ => throw new ArgumentOutOfRangeException(nameof(unit))
    };

    private static double FromKelvin(string unit, double kelvin) => unit switch
    {
        "celsius" => kelvin - 273.15,
        "fahrenheit" => (kelvin - 273.15) * 9.0 / 5.0 + 32,
        "kelvin" => kelvin,
        _ => throw new ArgumentOutOfRangeException(nameof(unit))
    };
}
