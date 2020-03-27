using System.Text.Json.Serialization;

namespace Frameio.NET.Models
{
    public class Error {

        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("detail")]
        public string Detail { get; set; }

        [JsonPropertyName("status")]
        public int Status { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

    }
}
