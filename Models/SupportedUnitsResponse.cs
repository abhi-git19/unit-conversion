namespace UnitConverterApi.Models;

/// <summary>
/// List of units supported for a given category.
/// </summary>
public class SupportedUnitsResponse
{
    public ConversionCategory Category { get; set; }
    public IEnumerable<string> Units { get; set; }
}
