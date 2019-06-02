using Newtonsoft.Json;

namespace Frameio.NET.Models
{
    public class Project
    {
        public string Id { get; set; }

        public string Name { get; set; }

        [JsonProperty("owner_id")]
        public string OwnerId { get; set; }

        public bool Private { get; set; }

        [JsonProperty("project_preferences")]
        public ProjectPreferences ProjectPreferences { get; set; }

        [JsonProperty("root_asset_id")]
        public string RootAssetId { get; set; }

        [JsonProperty("team_id")]
        public string TeamId { get; set; }

    }
}