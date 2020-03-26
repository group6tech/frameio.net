using System;
using Newtonsoft.Json;

namespace Frameio.NET.Models
{
    public class Asset
    {
        public string Id { get; set; }

        [JsonProperty("parent_id")]
        public string ParentId { get; set; }

        [JsonProperty("project_id")]
        public string ProjectId { get; set; }

        [JsonProperty("asset_type")]
        public string AssetType { get; set; }

        public string FileType { get; set; }

        public string Label { get; set; }

        public string Name { get; set; }

        public string Original { get; set; }

        public string Type { get; set; }

        [JsonProperty("view_count")]
        public int ViewCount { get; set; }

        [JsonProperty("upload_urls")]
        public string[] UploadUrls { get; set; }

    }
}
