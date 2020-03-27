using System.Text.Json.Serialization;

namespace Frameio.NET.Models
{
    public class Project
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("owner_id")]
        public string OwnerId { get; set; }

        [JsonPropertyName("private")]
        public bool Private { get; set; }

        [JsonPropertyName("project_preferences")]
        public ProjectPreferences ProjectPreferences { get; set; }

        [JsonPropertyName("root_asset_id")]
        public string RootAssetId { get; set; }

        [JsonPropertyName("team_id")]
        public string TeamId { get; set; }

    }
}
