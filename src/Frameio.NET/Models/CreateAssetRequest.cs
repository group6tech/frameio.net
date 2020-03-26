using Frameio.NET.Enums;
using Newtonsoft.Json;

namespace Frameio.NET.Models
{
    public class CreateAssetRequest
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("filesize")]
        public long FileSize { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public FileType Type { get; set; }

        [JsonProperty("filetype")]
        public string MimeType { get; set; }

    }
}
