using System.Net.Http;
using System.Threading.Tasks;
using Frameio.NET.Interfaces;
using Frameio.NET.Models;

namespace Frameio.NET
{
    public class Users : IUsers
    {
        private readonly IApiClient _client;

        public Users(IApiClient client)
        {
            _client = client;
        }

        public async Task<User> GetCurrentUser()
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "/v2/me");
            _client.SetAuthorizationHeader(request);

            HttpResponseMessage response = await _client.SendAsync(request);
            string content = await response.Content.ReadAsStringAsync();

            return _client.ParseJsonResponse<User>(response.StatusCode, content);
        }
    }
}
