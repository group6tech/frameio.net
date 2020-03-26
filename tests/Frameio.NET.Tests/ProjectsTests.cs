using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Frameio.NET.Models;
using Newtonsoft.Json;
using Xunit;

namespace Frameio.NET.Tests
{
    public class ProjectsTests
    {
        [Fact]
        public async Task GetProjects_Should_Return_PagedListOfProjectsWithOutPageLinks()
        {
            List<Project> projectList = new List<Project>
            {
                new Project
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Project 1",
                    OwnerId = Guid.NewGuid().ToString(),
                    Private = true,
                    ProjectPreferences = new ProjectPreferences(),
                    RootAssetId = Guid.NewGuid().ToString(),
                    TeamId = Guid.NewGuid().ToString()
                },

                new Project
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Project 2",
                    OwnerId = Guid.NewGuid().ToString(),
                    Private = true,
                    ProjectPreferences = new ProjectPreferences(),
                    RootAssetId = Guid.NewGuid().ToString(),
                    TeamId = Guid.NewGuid().ToString()
                },

                new Project
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Project 3",
                    OwnerId = Guid.NewGuid().ToString(),
                    Private = true,
                    ProjectPreferences = new ProjectPreferences(),
                    RootAssetId = Guid.NewGuid().ToString(),
                    TeamId = Guid.NewGuid().ToString()
                },

                new Project
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Project 4",
                    OwnerId = Guid.NewGuid().ToString(),
                    Private = true,
                    ProjectPreferences = new ProjectPreferences(),
                    RootAssetId = Guid.NewGuid().ToString(),
                    TeamId = Guid.NewGuid().ToString()
                }
            };

            HttpResponseMessage responseMessage = new HttpResponseMessage
            {
                Content = new StringContent(JsonConvert.SerializeObject(projectList), Encoding.UTF8, "application/json"),
                StatusCode = HttpStatusCode.OK
            };
            responseMessage.Headers.Add("total-pages", new List<string> { "1" });
            responseMessage.Headers.Add("total", new List<string> { "4" });

            FakeHttpMessageHandler fakeHttpMessageHandler = new FakeHttpMessageHandler(responseMessage);

            HttpClient fakeHttpClient = new HttpClient(fakeHttpMessageHandler)
            {
                BaseAddress = new Uri("http://Fake.domain.com")
            };
            ApiClient client = new ApiClient(fakeHttpClient);

            Projects projectClient = new Projects(client);
            string fakeTeamId = Guid.NewGuid().ToString("D");
            PagedResult<Project> pagedAssetsResult = await projectClient.GetProjects(fakeTeamId, 1);

            Assert.Equal(4, pagedAssetsResult.Results.Count());
            Assert.Equal(1, pagedAssetsResult.Paging.TotalPages);
            Assert.Equal(4, pagedAssetsResult.Paging.TotalRecords);

            Assert.Null(pagedAssetsResult.Paging.LastLink);
            Assert.Null(pagedAssetsResult.Paging.NextLink);
            Assert.Null(pagedAssetsResult.Paging.PreviousLink);

            Assert.Equal("Project 1", pagedAssetsResult.Results.First().Name);
            Assert.Equal("Project 4", pagedAssetsResult.Results.Last().Name);
        }

    }
}
