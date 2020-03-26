using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Frameio.NET.Enums;
using Frameio.NET.Models;
using Newtonsoft.Json;
using Xunit;

namespace Frameio.NET.Tests
{
    public class AssetsTests
    {
        [Fact]
        public async Task GetChildren_Should_Return_3_Assets()
        {
            List<Asset> assetsList = new List<Asset>
            {
                new Asset
                {
                    Name = "Asset 1",
                    Type = "",
                    AssetType = "",
                    Id = Guid.NewGuid().ToString("D"),
                    Label = "My Label for Asset 1",
                    Original = "",
                    ParentId = Guid.NewGuid().ToString("D"),
                    ProjectId = Guid.NewGuid().ToString("D"),
                    UploadUrls = Array.Empty<string>(),
                    ViewCount = 0
                },
                new Asset
                {
                    Name = "Asset 2",
                    Type = "",
                    AssetType = "",
                    Id = Guid.NewGuid().ToString("D"),
                    Label = "My Label for Asset 2",
                    Original = "",
                    ParentId = Guid.NewGuid().ToString("D"),
                    ProjectId = Guid.NewGuid().ToString("D"),
                    UploadUrls = Array.Empty<string>(),
                    ViewCount = 0
                },
                new Asset
                {
                    Name = "Asset 3",
                    Type = "",
                    AssetType = "",
                    Id = Guid.NewGuid().ToString("D"),
                    Label = "My Label for Asset 3",
                    Original = "",
                    ParentId = Guid.NewGuid().ToString("D"),
                    ProjectId = Guid.NewGuid().ToString("D"),
                    UploadUrls = Array.Empty<string>(),
                    ViewCount = 0
                }
            };

            HttpResponseMessage responseMessage = new HttpResponseMessage
            {
                Content = new StringContent(JsonConvert.SerializeObject(assetsList), Encoding.UTF8, "application/json"),
                StatusCode = HttpStatusCode.OK
            };
            responseMessage.Headers.Add("total-pages", new List<string> { "1" });
            responseMessage.Headers.Add("total", new List<string> { "3" });
            responseMessage.Headers.Add("link", new List<string> { "<https://applications.frame.io:80/v2/assets/e48430cd-7be7-416d-87d4-0290e10c72ba/children?page=1&page_size=10>; rel='first', <https://applications.frame.io:80/v2/assets/e48430cd-7be7-416d-87d4-0290e10c72ba/children?page=1&page_size=10>; rel='last'" });

            FakeHttpMessageHandler fakeHttpMessageHandler = new FakeHttpMessageHandler(responseMessage);

            HttpClient fakeHttpClient = new HttpClient(fakeHttpMessageHandler)
            {
                BaseAddress = new Uri("http://Fake.domain.com")
            };

            ApiClient client = new ApiClient(fakeHttpClient);
            Assets assetsClient = new Assets(client);

            string fakeAssetId = Guid.NewGuid().ToString("D");
            PagedResult<Asset> pagedAssetsResult = await assetsClient.GetChildren(fakeAssetId);

            Assert.Equal(3, pagedAssetsResult.Results.Count());
            Assert.Equal(1, pagedAssetsResult.Paging.TotalPages);
            Assert.Equal(3, pagedAssetsResult.Paging.TotalRecords);

            Assert.Null(pagedAssetsResult.Paging.LastLink);
            Assert.Null(pagedAssetsResult.Paging.NextLink);
            Assert.Null(pagedAssetsResult.Paging.PreviousLink);
        }

        [Fact]
        public async Task CreateAsset_Should_Return_Asset()
        {
            string parentId = Guid.NewGuid().ToString("D");
            string projectId = Guid.NewGuid().ToString("D");
            string assetName = "Asset 1";
            string assetDescription = "Asset 1 description";

            Asset expectedAsset = new Asset
            {
                Id = Guid.NewGuid().ToString("D"),
                Label = assetDescription,
                Name = assetName,
                ParentId = parentId,
                ProjectId = projectId,
                Type ="file",
                UploadUrls = new[] {
                    "http://Fake.domain.com/Chunk1",
                    "http://Fake.domain.com/Chunk2",
                    "http://Fake.domain.com/Chunk3"
                },
                ViewCount = 0
            };

            HttpResponseMessage responseMessage = new HttpResponseMessage
            {
                Content = new StringContent(JsonConvert.SerializeObject(expectedAsset), Encoding.UTF8, "application/json"),
                StatusCode = HttpStatusCode.OK
            };
            FakeHttpMessageHandler fakeHttpMessageHandler = new FakeHttpMessageHandler(responseMessage);

            HttpClient fakeHttpClient = new HttpClient(fakeHttpMessageHandler)
            {
                BaseAddress = new Uri("http://Fake.domain.com")
            };
            ApiClient client = new ApiClient(fakeHttpClient);

            Assets assetsClient = new Assets(client);

            var assetResponse = await assetsClient.CreateAsset(parentId, new CreateAssetRequest
            {
                Type = FileType.File,
                Name = assetName,
                FileSize = 1024,
                MimeType = "",
                Description = assetDescription
            });

            Assert.NotNull(assetResponse);
            Assert.Equal(3, assetResponse.UploadUrls.Length);
            Assert.Equal(assetName, assetResponse.Name);
            Assert.Equal(assetDescription, assetResponse.Label);
            Assert.Equal(projectId, assetResponse.ProjectId);
            Assert.Equal(parentId, assetResponse.ParentId);
        }
    }
}
