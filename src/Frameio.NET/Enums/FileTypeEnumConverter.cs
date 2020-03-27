using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Frameio.NET.Enums
{
    internal class FileTypeEnumConverter : JsonConverter<FileType>
    {
        public override FileType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, FileType value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString().ToLower());
        }
    }
}
