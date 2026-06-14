using UnitConverterApi.Exceptions;
using UnitConverterApi.Models;
using UnitConverterApi.Services.Strategies;

namespace UnitConverterApi.Services;

/// <summary>
/// On-demand factory that resolves strategies from the DI container only when
/// the request actually needs them. Each strategy is registered as a keyed
/// singleton under its <see cref="ConversionCategory"/> key, so the very first
/// request for a category constructs its strategy; all subsequent requests reuse
/// the same instance. Categories that are never requested are never instantiated.
/// </summary>
public class ConversionStrategyFactory : IConversionStrategyFactory
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IReadOnlyCollection<ConversionCategory> _registeredCategories;

    /// <param name="serviceProvider">Used to pull the keyed strategy on demand.</param>
    /// <param name="registeredCategories">
    ///     The set of categories that have a keyed strategy registered.
    ///     Used only by <see cref="GetAllStrategies"/> to enumerate every strategy
    ///     without having to resolve them all upfront.
    /// </param>
    public ConversionStrategyFactory(
        IServiceProvider serviceProvider,
        IEnumerable<ConversionCategory> registeredCategories)
    {
        _serviceProvider = serviceProvider;
        _registeredCategories = registeredCategories.ToList();
    }

    /// <summary>
    /// Resolves the strategy for <paramref name="category"/> from the DI container.
    /// Only this category's strategy is instantiated — nothing else.
    /// </summary>
    public IConversionStrategy GetStrategy(ConversionCategory category)
    {
        var strategy = _serviceProvider.GetKeyedService<IConversionStrategy>(category);

        return strategy ?? throw new UnsupportedCategoryException(category.ToString());
    }

    /// <summary>
    /// Resolves all registered strategies on demand (e.g. for the /units listing endpoint).
    /// Each strategy is still only instantiated the first time it is requested.
    /// </summary>
    public IEnumerable<IConversionStrategy> GetAllStrategies() =>
        _registeredCategories
            .Select(c => _serviceProvider.GetKeyedService<IConversionStrategy>(c))
            .OfType<IConversionStrategy>();
}
