using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Frameio.NET.Models;

namespace Frameio.NET.Interfaces
{
    public interface IApiClient
    {
        /// <summary>
        /// Returns a paged 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="headers"></param>
        /// <param name="statusCode"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        PagedResult<T> ParsePagedResponse<T>(HttpResponseHeaders headers, HttpStatusCode statusCode, string response);

        /// <summary>
        /// Parse a json response and returns T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="statusCode"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        T ParseJsonResponse<T>(HttpStatusCode statusCode, string response);

        /// <summary>
        /// Parse an Xml response and returns T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="statusCode"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        string ParseXmlResponse(HttpStatusCode statusCode, string response);

        /// <summary>
        /// Send an HTTP request and returns the response
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);

        /// <summary>
        /// Sets the authorization header on the given request
        /// </summary>
        /// <param name="request"></param>
        void SetAuthorizationHeader(HttpRequestMessage request);

        /// <summary>
        /// Stores the Authorization token for use with SetAuthorizationHeader
        /// </summary>
        /// <param name="authorizationToken"></param>
        void SetAuthorizationToken(string authorizationToken);

    }
}
