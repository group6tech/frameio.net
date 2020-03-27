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

        private string _authorizationToken;

        public ApiClient(HttpClient client)
        {
            _authorizationToken = string.Empty;
            _client = client;
        }

        public PagedResult<T> ParsePagedResponse<T>(HttpResponseHeaders headers, HttpStatusCode statusCode, string response)
        {
            return ResponseParser.ParsePagedResponse<T>(headers, statusCode, response);
        }

        public T ParseJsonResponse<T>(HttpStatusCode statusCode, string response)
        {
            return ResponseParser.ParseJsonResponse<T>(statusCode, response);
        }

        public string ParseXmlResponse(HttpStatusCode statusCode, string response)
        {
            return ResponseParser.ParseXmlResponse(statusCode, response);
        }

        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            if (!request.RequestUri.ToString().StartsWith("https://"))
            {
                request.RequestUri = new Uri($"https://api.frame.io{request.RequestUri}");
            }
            return await _client.SendAsync(request);
        }

        public void SetAuthorizationHeader(HttpRequestMessage request)
        {
            if (string.IsNullOrWhiteSpace(_authorizationToken))
            {
                return;
            }

            request.Headers.Add("User-Agent", "Frameio.Net");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _authorizationToken);
        }

        public void SetAuthorizationToken(string authorizationToken)
        {
            _authorizationToken = authorizationToken;
        }
    }
}
