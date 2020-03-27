using Frameio.NET.Interfaces;
using Frameio.NET.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Frameio.NET
{
    public class Assets : IAssets
    {

        private readonly IApiClient _client;

        public Assets(IApiClient client)
        {
            _client = client;
        }

        private async Task<string> UploadChunk(string url, byte[] bytes, string contentType)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, url);

            ByteArrayContent byteContent = new ByteArrayContent(bytes);
            request.Content = byteContent;

            request.Content.Headers.Add("content-type", contentType);
            request.Content.Headers.Add("x-amz-acl", "private");

            HttpResponseMessage response = await _client.SendAsync(request);

            var content = await response.Content.ReadAsStringAsync();

            return _client.ParseXmlResponse(response.StatusCode, content);
        }

        public async Task<PagedResult<Asset>> GetChildren(string assetId, int page, int pageSize = 10)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"/v2/assets/{assetId}/children?page_size={pageSize}&page={page}");
            _client.SetAuthorizationHeader(request);

            HttpResponseMessage response = await _client.SendAsync(request);
            string content = await response.Content.ReadAsStringAsync();

            return _client.ParsePagedResponse<Asset>(response.Headers, response.StatusCode, content);
        }

        public async Task<Asset> CreateAsset(string parentId, CreateAssetRequest assetRequest)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"/v2/assets/{parentId}/children");
            _client.SetAuthorizationHeader(request);

            string serialized = JsonSerializer.Serialize(assetRequest);
            request.Content = new StringContent(serialized, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            return _client.ParseJsonResponse<Asset>(response.StatusCode, content);
        }

        public string UploadAsset(Asset asset, string fileName)
        {
            var fileLength = new FileInfo(fileName).Length;
            var roughLength = Math.Floor((double)(fileLength / asset.UploadUrls.Length));

            if (roughLength > int.MaxValue)
            {
                throw new Exception($"Part size {roughLength} is too large, must be less than int.MaxValue.");
            }

            var partLength = (long)roughLength;
            var partNo = 0;
            long totalLengthSent = 0;
            var parts = new List<(int partNo, long offset, long length)>();
            while (totalLengthSent < fileLength)
            {
                if ((fileLength - totalLengthSent) < partLength)
                {
                    partLength = fileLength - totalLengthSent;
                }

                parts.Add((partNo, totalLengthSent, partLength));
                totalLengthSent += partLength;
                partNo++;
            }

            if (parts.Count != asset.UploadUrls.Length)
            {
                throw new Exception("Invalid part count");
            }

            var threads = asset.UploadUrls.Length < 10 ? asset.UploadUrls.Length : 10;

            using (var mmf = MemoryMappedFile.CreateFromFile(fileName, FileMode.Open))
            {
                parts.AsParallel()
                    .WithDegreeOfParallelism(threads)
                    .ForAll(part =>
                    {
                        using (var viewStream = mmf.CreateViewStream(part.offset, part.length))
                        {
                            var uri = asset.UploadUrls[part.partNo];
                            var content = new StreamContent(viewStream);

                            var bytes = content.ReadAsByteArrayAsync().Result;

                            var result = UploadChunk(uri, bytes, asset.FileType).Result;

                            viewStream.Close();
                        }
                    });
            }

            return string.Empty;
        }
    }
}
