using System.Text.Json.Serialization;

namespace Frameio.NET.Models
{
    public class Asset
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("parent_id")]
        public string ParentId { get; set; }

        [JsonPropertyName("project_id")]
        public string ProjectId { get; set; }

        [JsonPropertyName("asset_type")]
        public string AssetType { get; set; }

        [JsonPropertyName("filetype")]
        public string FileType { get; set; }

        [JsonPropertyName("label")]
        public string Label { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("original")]
        public string Original { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("view_count")]
        public int ViewCount { get; set; }

        [JsonPropertyName("upload_urls")]
        public string[] UploadUrls { get; set; }

    }
}
