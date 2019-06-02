using Newtonsoft.Json;

namespace Frameio.NET.Models
{
    public class Team
    {
        public string Id { get; set; }

        public string Access { get; set; }

        [JsonProperty("file_count")]
        public int FileCount { get; set; }

        [JsonProperty("collaborator_count")]
        public int CollaboratorCount { get; set; }

        public string Name { get; set; }

        [JsonProperty("project_count")]
        public int ProjectCount { get; set; }

        public int Storage { get; set; }

    }
}