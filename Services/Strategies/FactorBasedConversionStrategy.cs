using UnitConverterApi.Exceptions;
using UnitConverterApi.Models;

namespace UnitConverterApi.Services.Strategies;

/// <summary>
/// Base class for categories where every unit can be converted to/from a common
/// "base unit" via a simple multiplicative factor (length, weight, volume, area).
/// Concrete classes only need to supply the factor table.
/// </summary>
public abstract class FactorBasedConversionStrategy : IConversionStrategy
{
    /// <summary>
    /// Maps each supported unit name (lower-case) to the number of base units
    /// it represents. E.g. for length with "meter" as base: "kilometer" -> 1000, "centimeter" -> 0.01.
    /// </summary>
    protected abstract IReadOnlyDictionary<string, double> UnitFactors { get; }

    public abstract ConversionCategory Category { get; }

    public IReadOnlyCollection<string> SupportedUnits => [.. UnitFactors.Keys];

    public double Convert(string fromUnit, string toUnit, double value)
    {
        var from = NormalizeAndValidate(fromUnit);
        var to = NormalizeAndValidate(toUnit);

        // Convert input to the base unit, then from the base unit to the target unit.
        var valueInBaseUnit = value * UnitFactors[from];
        return valueInBaseUnit / UnitFactors[to];
    }

    private string NormalizeAndValidate(string unit)
    {
        var normalized = unit.Trim().ToLowerInvariant();
        if (!UnitFactors.ContainsKey(normalized))
        {
            throw new UnsupportedUnitException(unit, Category.ToString());
        }
        return normalized;
    }
}
