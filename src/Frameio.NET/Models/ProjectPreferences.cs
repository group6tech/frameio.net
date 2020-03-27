using System.Text.Json.Serialization;

namespace Frameio.NET.Models
{
    public class ProjectPreferences
    {
        [JsonPropertyName("notify_on_updated_label")]
        public bool NotifyOnUpdatedLabel { get; set; }

        [JsonPropertyName("notify_on_new_mention")]
        public bool NotifyOnNewMention { get; set; }

        [JsonPropertyName("notify_on_new_comment")]
        public bool NotifyOnNewComment { get; set; }

        [JsonPropertyName("notify_on_new_collaborator")]
        public bool NotifyOnNewCollaborator { get; set; }

        [JsonPropertyName("notify_on_new_asset")]
        public bool NotifyOnNewAsset { get; set; }

        [JsonPropertyName("collaborator_can_share")]
        public bool CollaboratorCanShare { get; set; }

        [JsonPropertyName("collaborator_can_invite")]
        public bool CollaboratorCanInvite { get; set; }

        [JsonPropertyName("collaborator_can_download")]
        public bool CollaboratorCanDownload { get; set; }

    }
}
