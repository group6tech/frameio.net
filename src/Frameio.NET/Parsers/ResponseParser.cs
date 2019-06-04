using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Xml.Serialization;
using Frameio.NET.Models;
using Newtonsoft.Json;

namespace Frameio.NET.Parsers
{
    public class ResponseParser
    {
        public T ParseJsonResponse<T>(HttpStatusCode statusCode, string response)
        {
            switch (statusCode)
            {
                case HttpStatusCode.OK:
                    return JsonConvert.DeserializeObject<T>(response);
                case (HttpStatusCode)429:
                case HttpStatusCode.NotFound:
                case HttpStatusCode.Unauthorized:
                case HttpStatusCode.Forbidden:
                case (HttpStatusCode)402:
                case HttpStatusCode.InternalServerError:
                    throw new FrameioException((int)statusCode, null, response);
                case (HttpStatusCode)422:
                    ErrorResponse errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(response);
                    throw new FrameioException(errorResponse.Code, errorResponse.Errors, errorResponse.Message);
                default:
                    ErrorResponse defaultResponse = JsonConvert.DeserializeObject<ErrorResponse>(response);
                    throw new FrameioException(defaultResponse.Code, defaultResponse.Errors, defaultResponse.Message);
            }
        }

        public string ParseXmlResponse(HttpStatusCode statusCode, string response)
        {
            if (statusCode == HttpStatusCode.OK) {
                return response;
            }

            TextReader reader = new StringReader(response);
            XmlSerializer contentXmlSerializer = new XmlSerializer(typeof(XmlError));

            XmlError xmlErrorResponse = (XmlError)contentXmlSerializer.Deserialize(reader);
            throw new FrameioException((int)statusCode, null, xmlErrorResponse.Message);
        }

        public PagedResult<T> ParsePagedResponse<T>(HttpResponseHeaders headers, HttpStatusCode statusCode, string response)
        {
            PagedResult<T> pagedResult = new PagedResult<T>();

            pagedResult.Paging = ParseHeader(headers);
            pagedResult.Results = ParseJsonResponse<IEnumerable<T>>(statusCode, response);

            return pagedResult;
        }

        public Paging ParseHeader(HttpResponseHeaders headers)
        {
            Paging paging = new Paging();

            if (headers.TryGetValues("total-pages", out var headerTotalPages))
            {
                if (int.TryParse(headerTotalPages.First(), out var totalPages))
                {
                    paging.TotalPages = totalPages;
                }
            }

            if (headers.TryGetValues("total", out var headerTotal))
            {
                if (int.TryParse(headerTotal.First(), out var total))
                {
                    paging.TotalRecords = total;
                }
            }

            if (headers.TryGetValues("link", out var link))
            {
                LinkHeaderParser headerParserLinks = new LinkHeaderParser(link.First());

                paging.FirstLink = headerParserLinks.FirstLink;
                paging.LastLink = headerParserLinks.LastLink;
                paging.NextLink = headerParserLinks.NextLink;
                paging.PreviousLink = headerParserLinks.PreviousLink;
            }

            return paging;
        }

    }
}
