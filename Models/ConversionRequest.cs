using System.ComponentModel.DataAnnotations;

namespace UnitConverterApi.Models;

/// <summary>
/// Incoming request to convert a value from one unit to another within a category.
/// </summary>
public class ConversionRequest
{
    /// <summary>The category of conversion, e.g. Length, Weight, Temperature.</summary>
    [Required]
    public ConversionCategory Category { get; set; }

    /// <summary>The unit the input value is expressed in (e.g. "meter", "celsius").</summary>
    [Required]
    [MinLength(1, ErrorMessage = "FromUnit is required.")]
    public string FromUnit { get; set; }

    /// <summary>The unit to convert the value into (e.g. "feet", "fahrenheit").</summary>
    [Required]
    [MinLength(1, ErrorMessage = "ToUnit is required.")]
    public string ToUnit { get; set; }

    /// <summary>The numeric value to convert.</summary>
    [Required]
    public double Value { get; set; }
}
