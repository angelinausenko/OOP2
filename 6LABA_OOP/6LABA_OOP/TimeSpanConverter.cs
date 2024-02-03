using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OOP_lab6
{
    public class TimeSpanJsonConverter : JsonConverter<TimeSpan>
    {
        public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                if (TimeSpan.TryParse(reader.GetString(),
                                    out var convertedValue))
                    return convertedValue;
            }
            return default;
        }

        public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
