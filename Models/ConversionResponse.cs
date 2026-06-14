namespace UnitConverterApi.Models;

/// <summary>
/// Result of a successful conversion.
/// </summary>
public class ConversionResponse
{
    public ConversionCategory Category { get; set; }
    public string FromUnit { get; set; }
    public string ToUnit { get; set; }
    public double InputValue { get; set; }
    public double ConvertedValue { get; set; }
}
