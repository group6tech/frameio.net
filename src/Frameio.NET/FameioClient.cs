using System.Net.Http;
using Frameio.NET.Interfaces;

namespace Frameio.NET
{
    public class FameioClient : IFrameIoClient
    {
        private readonly IApiClient _apiClient;

        private readonly Assets _assets;

        private readonly Projects _projects;

        private readonly Teams _teams;

        private readonly Users _users;

        public IAssets Assets => _assets;

        public IProjects Projects => _projects;

        public ITeams Teams => _teams;

        public IUsers Users => _users;

        public FameioClient(HttpClient client)
        {
            _apiClient = new ApiClient(client);
            _assets = new Assets(_apiClient);
            _projects = new Projects(_apiClient);
            _teams = new Teams(_apiClient);
            _users = new Users(_apiClient);
        }

        /// <summary>
        /// Sets the Authorization Token to be used with HTTP requests
        /// </summary>
        /// <param name="token"></param>
        public void Initialize(string token)
        {
            _apiClient.SetAuthorizationToken(token);
        }

    }
}
