using System.Text.Json.Serialization;

namespace Frameio.NET.Enums
{
    [JsonConverter(typeof(FileTypeEnumConverter))]
    public enum FileType
    {
        File,
        Folder
    }
}
