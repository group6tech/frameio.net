using Frameio.NET.Enums;
using System.Text.Json.Serialization;

namespace Frameio.NET.Models
{
    public class CreateAssetRequest
    {
        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("filesize")]
        public long FileSize { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("type")]
        public FileType Type { get; set; }

        [JsonPropertyName("filetype")]
        public string MimeType { get; set; }

    }
}
