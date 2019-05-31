using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Frameio.NET.Interfaces;
using Frameio.NET.Models;
using Newtonsoft.Json;

namespace Frameio.NET
{
    public class Assets : IAssets {

        private readonly IApiClient _client;

        public Assets(IApiClient client) {
            _client = client;
        }

        public async Task<PagedResult<Asset>> GetChildren(string assetId, int pageSize = 10, int page = 1)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"/v2/assets/{assetId}/children?page_size={pageSize}&page={page}");
            _client.SetAuthorizationHeader(request);

            HttpResponseMessage response = await _client.SendAsync(request);
            string content = await response.Content.ReadAsStringAsync();

            return _client.ParsePagedResponse<Asset>(response.Headers, response.StatusCode, content);
        }

        public async Task<Asset> CreateAsset(string parentId, CreateAssetRequest assetRequest)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"v2/assets/{parentId}/children");
            _client.SetAuthorizationHeader(request);

            string serialized = JsonConvert.SerializeObject(assetRequest);
            request.Content = new StringContent(serialized, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            return _client.ParseResponse<Asset>(response.StatusCode, content);
        }

        public async Task<string> UploadAsset(string url, byte[] bytes, string contentType)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, url);

            ByteArrayContent byteContent = new ByteArrayContent(bytes);
            request.Content = byteContent;

            request.Content = byteContent;
            request.Content.Headers.Add("content-type", contentType);
            request.Content.Headers.Add("x-amz-acl", "private");

            HttpResponseMessage response = await _client.SendAsync(request);

            var content = await response.Content.ReadAsStringAsync();

            return _client.ParseResponse<string>(response.StatusCode, content);
        }
    }
}
