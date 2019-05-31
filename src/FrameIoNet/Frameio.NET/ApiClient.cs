using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Frameio.NET.Interfaces;
using Frameio.NET.Models;
using Frameio.NET.Parsers;

namespace Frameio.NET {
    public class ApiClient : IApiClient
    {
        private readonly HttpClient _client;

        private readonly ResponseParser _responseParser;

        private string _authorizationToken;

        public ApiClient(HttpClient client)
        {
            _authorizationToken = string.Empty;

            _client = client;
            _client.BaseAddress = new Uri("https://applications.frame.io");
            _client.DefaultRequestHeaders.Add("User-Agent", "Frameio.Net");

            _responseParser = new ResponseParser();
        }

        public PagedResult<T> ParsePagedResponse<T>(HttpResponseHeaders headers, HttpStatusCode statusCode, string response)
        {
            return _responseParser.ParsePagedResponse<T>(headers, statusCode, response);
        }

        public T ParseResponse<T>(HttpStatusCode statusCode, string response)
        {
            return _responseParser.ParseResponse<T>(statusCode, response);
        }

        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            return await _client.SendAsync(request);
        }

        public void SetAuthorizationHeader(HttpRequestMessage request)
        {
            if (string.IsNullOrWhiteSpace(_authorizationToken))
            {
                return;
            }

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _authorizationToken);
        }

        public void SetAuthorizationToken(string authorizationToken)
        {
            _authorizationToken = authorizationToken;
        }
    }
}
