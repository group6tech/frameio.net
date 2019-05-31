using System.Net.Http;
using System.Threading.Tasks;
using Frameio.NET.Interfaces;
using Frameio.NET.Models;

namespace Frameio.NET
{
    public class Teams : ITeams
    {
        private readonly IApiClient _client;

        public Teams(IApiClient client)
        {
            _client = client;
        }

        public async Task<PagedResult<Team>> GetTeams(string accountId, int pageSize = 10, int page = 1)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"/v2/accounts/{accountId}/teams?page_size={pageSize}&page={page}");
            _client.SetAuthorizationHeader(request);

            HttpResponseMessage response = await _client.SendAsync(request);
            string content = await response.Content.ReadAsStringAsync();

            return _client.ParsePagedResponse<Team>(response.Headers, response.StatusCode, content);
        }

    }
}
