using UnitConverterApi.Models;

namespace UnitConverterApi.Services;

public interface IConversionService
{
    ConversionResponse Convert(ConversionRequest request);
    SupportedUnitsResponse GetSupportedUnits(ConversionCategory category);
    IEnumerable<SupportedUnitsResponse> GetAllSupportedUnits();
}
