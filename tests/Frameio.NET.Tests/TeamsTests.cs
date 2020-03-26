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
    public class TeamsTests
    {
        [Fact]
        public async Task GetTeams_Should_Return_PagedListOfTeamsWithOutPageLinks()
        {
            List<Team> teamsList = new List<Team>
            {
                new Team
                {
                    Id = Guid.NewGuid().ToString(),
                    Access = "private",
                    CollaboratorCount = 0,
                    FileCount = 10,
                    Name = "Team 1",
                    ProjectCount = 1,
                    Storage = 0
                },

                new Team
                {
                    Id = Guid.NewGuid().ToString(),
                    Access = "private",
                    CollaboratorCount = 0,
                    FileCount = 10,
                    Name = "Team 2",
                    ProjectCount = 1,
                    Storage = 0
                },

                new Team
                {
                    Id = Guid.NewGuid().ToString(),
                    Access = "private",
                    CollaboratorCount = 0,
                    FileCount = 10,
                    Name = "Team 3",
                    ProjectCount = 1,
                    Storage = 0
                }
            };

            HttpResponseMessage responseMessage = new HttpResponseMessage
            {
                Content = new StringContent(JsonConvert.SerializeObject(teamsList), Encoding.UTF8, "application/json"),
                StatusCode = HttpStatusCode.OK
            };
            responseMessage.Headers.Add("total-pages", new List<string> { "1" });
            responseMessage.Headers.Add("total", new List<string> { "3" });

            FakeHttpMessageHandler fakeHttpMessageHandler = new FakeHttpMessageHandler(responseMessage);

            HttpClient fakeHttpClient = new HttpClient(fakeHttpMessageHandler)
            {
                BaseAddress = new Uri("http://Fake.domain.com")
            };
            ApiClient client = new ApiClient(fakeHttpClient);

            Teams teamsClient = new Teams(client);
            string fakeAccountId = Guid.NewGuid().ToString("D");
            PagedResult<Team> pagedAssetsResult = await teamsClient.GetTeams(fakeAccountId, 1);

            Assert.Equal(3, pagedAssetsResult.Results.Count());
            Assert.Equal(1, pagedAssetsResult.Paging.TotalPages);
            Assert.Equal(3, pagedAssetsResult.Paging.TotalRecords);

            Assert.Null(pagedAssetsResult.Paging.LastLink);
            Assert.Null(pagedAssetsResult.Paging.NextLink);
            Assert.Null(pagedAssetsResult.Paging.PreviousLink);

            Assert.Equal("Team 1", pagedAssetsResult.Results.First().Name);
            Assert.Equal("Team 3", pagedAssetsResult.Results.Last().Name);
        }

    }
}
