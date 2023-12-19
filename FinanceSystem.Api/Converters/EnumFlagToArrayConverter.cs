using FinanceSystem.Common.Enums;
using Newtonsoft.Json;

namespace FinanceSystem.Converters;

public class EnumFlagToArrayConverter : JsonConverter<PaymentTypes>
{
    public override void WriteJson(JsonWriter writer, PaymentTypes value, JsonSerializer serializer)
    {
        var flags = Enum.GetValues(typeof(PaymentTypes))
            .Cast<PaymentTypes>()
            .Where(x => value.HasFlag(x))
            .Select(x => x.ToString())
            .ToArray();
        
        writer.WriteStartArray();

        foreach (var flag in flags)
            writer.WriteValue(flag);
        
        writer.WriteEndArray();
    }

    public override PaymentTypes ReadJson(JsonReader reader, Type objectType, PaymentTypes existingValue, bool hasExistingValue,
        JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null)
            return default;

        if (reader.TokenType != JsonToken.StartArray)
            throw new JsonSerializationException($"Unexpected token type '{reader.TokenType}' when reading flag enum.");

        var values = serializer.Deserialize<List<string>>(reader);

        if (!values.Any())
            return default;

        PaymentTypes result = default;

        foreach (var value in values)
        {
            if (!Enum.TryParse<PaymentTypes>(value, ignoreCase: true, out var enumValue))
                throw new ArgumentException($"Передано некорректное значение для {nameof(PaymentTypes)}");
           
            result |= enumValue;
        }

        return result;
    }
}

public class NullableEnumFlagToArrayConverter : JsonConverter<PaymentTypes?>
{
    public override void WriteJson(JsonWriter writer, PaymentTypes? value, JsonSerializer serializer)
    {
        if (!value.HasValue)
            return;
        
        var flags = Enum.GetValues(typeof(PaymentTypes))
            .Cast<PaymentTypes>()
            .Where(x => value.Value.HasFlag(x))
            .Select(x => x.ToString())
            .ToArray();
        
        writer.WriteStartArray();

        foreach (var flag in flags)
            writer.WriteValue(flag);
        
        writer.WriteEndArray();
    }

    public override PaymentTypes? ReadJson(JsonReader reader, Type objectType, PaymentTypes? existingValue, bool hasExistingValue,
        JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null)
            return default;

        if (reader.TokenType != JsonToken.StartArray)
            throw new JsonSerializationException($"Unexpected token type '{reader.TokenType}' when reading flag enum.");

        var values = serializer.Deserialize<List<string>>(reader);

        if (!values.Any())
            return default;

        PaymentTypes result = default;

        foreach (var value in values)
        {
            if (!Enum.TryParse<PaymentTypes>(value, ignoreCase: true, out var enumValue))
                throw new ArgumentException($"Передано некорректное значение для {nameof(PaymentTypes)}");
           
            result |= enumValue;
        }

        return result;
    }
}