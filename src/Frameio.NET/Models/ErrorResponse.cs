using System.Text.Json.Serialization;

namespace Frameio.NET.Models {
    public class ErrorResponse
    {
        [JsonPropertyName("code")]
        public int Code { get; set; }

        [JsonPropertyName("errors")]
        public Error[] Errors { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

    }
}
