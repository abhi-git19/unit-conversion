using System.Text.Json;
using System.Text.Json.Serialization;

namespace UnitConverterApi.Infrastructure.JsonConverters;

public class CaseInsensitiveEnumConverter<TEnum> : JsonConverter<TEnum>
    where TEnum : struct, Enum
{
    public override TEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.TokenType switch
        {
            JsonTokenType.String => ParseFromString(reader.GetString()),
            JsonTokenType.Number => ParseFromInt(reader.GetInt32()),
            _ => throw new JsonException(
                $"Expected a string or integer for {typeof(TEnum).Name}, " +
                $"but got {reader.TokenType}.")
        };
    }

    public override void Write(Utf8JsonWriter writer, TEnum value, JsonSerializerOptions options)
        => writer.WriteStringValue(value.ToString());

    private static TEnum ParseFromString(string raw)
    {
        if (string.IsNullOrWhiteSpace(raw))
            throw new JsonException(BuildError($"null or empty string"));

        if (Enum.TryParse<TEnum>(raw.Trim(), ignoreCase: true, out var result))
            return result;

        throw new JsonException(BuildError($"\"{raw}\""));
    }

    private static TEnum ParseFromInt(int value)
    {
        if (Enum.IsDefined(typeof(TEnum), value))
            return (TEnum)(object)value;

        throw new JsonException(BuildError(value.ToString()));
    }

    private static string BuildError(string received) =>
        $"'{received}' is not a valid {typeof(TEnum).Name}. " +
        $"Accepted values (case-insensitive): {string.Join(", ", Enum.GetNames<TEnum>())}.";
}
