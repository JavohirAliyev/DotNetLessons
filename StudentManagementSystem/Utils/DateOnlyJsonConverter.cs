using System.Text.Json;
using System.Text.Json.Serialization;

namespace StudentManagementSystem.Utils;

public class DateOnlyJsonConverter : JsonConverter<DateOnly>
{
    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var dateString = reader.GetString();
        if (DateOnly.TryParse(dateString, out var date))
        {
            return date;
        }
        if (DateTime.TryParse(dateString, out var dt))
        {
            return DateOnly.FromDateTime(dt);
        }
        throw new JsonException($"Invalid date format: {dateString}");
    }

    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString("yyyy-MM-dd"));
    }
}
