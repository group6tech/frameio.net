using Newtonsoft.Json;

namespace Frameio.NET.Models {
    public class ProjectPreferences
    {
        [JsonProperty("notify_on_updated_label")]
        public bool NotifyOnUpdatedLabel { get; set; }

        [JsonProperty("notify_on_new_mention")]
        public bool NotifyOnNewMention { get; set; }

        [JsonProperty("notify_on_new_comment")]
        public bool NotifyOnNewComment { get; set; }

        [JsonProperty("notify_on_new_collaborator")]
        public bool NotifyOnNewCollaborator { get; set; }

        [JsonProperty("notify_on_new_asset")]
        public bool NotifyOnNewAsset { get; set; }

        [JsonProperty("collaborator_can_share")]
        public bool CollaboratorCanShare { get; set; }

        [JsonProperty("collaborator_can_invite")]
        public bool CollaboratorCanInvite { get; set; }

        [JsonProperty("collaborator_can_download")]
        public bool CollaboratorCanDownload { get; set; }

    }
}