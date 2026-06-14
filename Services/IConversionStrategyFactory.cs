using UnitConverterApi.Models;
using UnitConverterApi.Services.Strategies;

namespace UnitConverterApi.Services;

/// <summary>
/// Resolves the appropriate <see cref="IConversionStrategy"/> for a given
/// <see cref="ConversionCategory"/> (Factory design pattern).
/// </summary>
public interface IConversionStrategyFactory
{
    IConversionStrategy GetStrategy(ConversionCategory category);
    IEnumerable<IConversionStrategy> GetAllStrategies();
}
