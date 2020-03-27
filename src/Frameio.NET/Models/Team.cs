using System.Text.Json.Serialization;

namespace Frameio.NET.Models
{
    public class Team
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("access")]
        public string Access { get; set; }

        [JsonPropertyName("file_count")]
        public int FileCount { get; set; }

        [JsonPropertyName("collaborator_count")]
        public int CollaboratorCount { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("project_count")]
        public int ProjectCount { get; set; }

        [JsonPropertyName("storage")]
        public long Storage { get; set; }

    }
}
