using Newtonsoft.Json;

namespace Frameio.NET.Models
{
    public class User
    {
        public string Id { get; set; }

        [JsonProperty("account_id")]
        public string AccountId { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

    }
}