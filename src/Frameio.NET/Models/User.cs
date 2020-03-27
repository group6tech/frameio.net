using System.Text.Json.Serialization;

namespace Frameio.NET.Models
{
    public class User
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("account_id")]
        public string AccountId { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

    }
}
