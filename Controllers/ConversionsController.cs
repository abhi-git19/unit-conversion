using Microsoft.AspNetCore.Mvc;
using UnitConverterApi.Models;
using UnitConverterApi.Services;

namespace UnitConverterApi.Controllers;

/// <summary>
/// Endpoints for converting values between units and discovering supported units.
/// </summary>
[ApiController]
[Route("api/v1/conversions")]
[Produces("application/json")]
public class ConversionsController(IConversionService conversionService) : ControllerBase
{
    private readonly IConversionService _conversionService = conversionService;

    /// <summary>
    /// Converts a value from one unit to another within a category
    /// (e.g. Length, Weight, Temperature, Volume, Area).
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /api/v1/conversions/convert
    ///     {
    ///        "category": "Temperature",
    ///        "fromUnit": "celsius",
    ///        "toUnit": "fahrenheit",
    ///        "value": 100
    ///     }
    /// </remarks>
    [HttpPost("convert")]
    [ProducesResponseType(typeof(ConversionResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public ActionResult<ConversionResponse> Convert([FromBody] ConversionRequest request)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var result = _conversionService.Convert(request);
        return Ok(result);
    }

    /// <summary>
    /// Returns the list of units supported for a single conversion category.
    /// </summary>
    [HttpGet("{category}/units")]
    [ProducesResponseType(typeof(SupportedUnitsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<SupportedUnitsResponse> GetSupportedUnits(ConversionCategory category)
    {
        var result = _conversionService.GetSupportedUnits(category);
        return Ok(result);
    }

    /// <summary>
    /// Returns supported units for every conversion category.
    /// </summary>
    [HttpGet("units")]
    [ProducesResponseType(typeof(IEnumerable<SupportedUnitsResponse>), StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<SupportedUnitsResponse>> GetAllSupportedUnits()
    {
        return Ok(_conversionService.GetAllSupportedUnits());
    }
}
