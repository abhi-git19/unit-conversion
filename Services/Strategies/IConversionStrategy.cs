using UnitConverterApi.Models;

namespace UnitConverterApi.Services.Strategies;

/// <summary>
/// Strategy contract for converting a value between two units within
/// a single measurement category (Strategy design pattern).
/// </summary>
public interface IConversionStrategy
{
    /// <summary>The category this strategy handles.</summary>
    ConversionCategory Category { get; }

    /// <summary>The set of unit names this strategy understands (case-insensitive).</summary>
    IReadOnlyCollection<string> SupportedUnits { get; }

    /// <summary>
    /// Converts <paramref name="value"/> from <paramref name="fromUnit"/> to <paramref name="toUnit"/>.
    /// </summary>
    /// <exception cref="Exceptions.UnsupportedUnitException">If either unit is unknown.</exception>
    /// <exception cref="Exceptions.InvalidConversionValueException">If the value is out of a valid domain.</exception>
    double Convert(string fromUnit, string toUnit, double value);
}
