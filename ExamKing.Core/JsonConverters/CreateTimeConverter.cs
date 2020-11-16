using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using ExamKing.Core.Utils;
using Fur.DependencyInjection;

namespace ExamKing.Core.JsonConverters
{
    [SkipScan]
    public class CreateTimeConverter : JsonConverter<string>
    {
        public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return reader.GetString();
        }

        public override void Write(Utf8JsonWriter writer, string? value, JsonSerializerOptions options)
        {
            var dateTime = TimeUtil.GetDateTime(value);
            writer.WriteStringValue(dateTime.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }
}