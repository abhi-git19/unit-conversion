namespace UnitConverterApi.Exceptions;

/// <summary>
/// Base type for all conversion-related errors. Caught centrally and
/// translated into appropriate HTTP responses.
/// </summary>
public abstract class ConversionException : Exception
{
    protected ConversionException(string message) : base(message) { }
}

/// <summary>
/// Thrown when a requested unit is not recognized for the given category.
/// </summary>
public class UnsupportedUnitException : ConversionException
{
    public UnsupportedUnitException(string unit, string category)
        : base($"Unit '{unit}' is not supported for category '{category}'.") { }
}

/// <summary>
/// Thrown when no conversion strategy is registered for a requested category.
/// </summary>
public class UnsupportedCategoryException : ConversionException
{
    public UnsupportedCategoryException(string category)
        : base($"Conversion category '{category}' is not supported.") { }
}

/// <summary>
/// Thrown when a value is invalid for the requested conversion
/// (e.g. a temperature below absolute zero).
/// </summary>
public class InvalidConversionValueException : ConversionException
{
    public InvalidConversionValueException(string message) : base(message) { }
}
