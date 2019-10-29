﻿using System.Net.Http;
using System.Threading.Tasks;
using Frameio.NET.Interfaces;
using Frameio.NET.Models;

namespace Frameio.NET
{
    public class Projects : IProjects
    {
        private readonly IApiClient _client;

        public Projects(IApiClient client)
        {
            _client = client;
        }

        public async Task<Project> GetProject(string projectId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"/v2/projects/{projectId}");
            _client.SetAuthorizationHeader(request);

            var response = await _client.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            return _client.ParseJsonResponse<Project>(response.StatusCode, content);
        }

        public async Task<PagedResult<Project>> GetProjects(string teamId, int page, int pageSize = 10)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"/v2/teams/{teamId}/projects?page_size={pageSize}&page={page}");
            _client.SetAuthorizationHeader(request);

            HttpResponseMessage response = await _client.SendAsync(request);
            string content = await response.Content.ReadAsStringAsync();

            return _client.ParsePagedResponse<Project>(response.Headers, response.StatusCode, content);
        }
    }
}
