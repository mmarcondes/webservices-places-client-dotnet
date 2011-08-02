using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;

namespace Maplink.Webservices.Places.Client.Wrappers
{
    public class HttpClient : IHttpClient
    {
        private static readonly IEnumerable<int> SuccessfulCodes = new List<int>
                                             {
                                                 (int) HttpStatusCode.OK,
                                                 (int) HttpStatusCode.Created,
                                                 (int) HttpStatusCode.Accepted,
                                                 (int) HttpStatusCode.NonAuthoritativeInformation,
                                                 (int) HttpStatusCode.NoContent,
                                                 (int) HttpStatusCode.ResetContent,
                                                 (int) HttpStatusCode.PartialContent
                                             };

        public Response Get(Request request)
        {
            var webRequest = (HttpWebRequest)WebRequest.Create(request.Uri);
            AddHeaders(webRequest.Headers, request.Headers);
            webRequest.KeepAlive = false;
            webRequest.Method = "GET";
            webRequest.ContentType = request.ContentType;

            var response = ProcessRequest(webRequest);

            return response;
        }

        private static void AddHeaders(
            NameValueCollection headers,
            IEnumerable<KeyValuePair<string, string>> requestHeaders)
        {
            foreach (var header in requestHeaders)
            {
                headers.Add(header.Key, header.Value);
            }
        }

        private static Response ProcessRequest(WebRequest webRequest)
        {
            string body;
            int statusCode;
            IEnumerable<KeyValuePair<string, string>> headers;

            try
            {
                using (var webResponse = (HttpWebResponse)webRequest.GetResponse())
                {
                    statusCode = (int)webResponse.StatusCode;
                    headers = ExtractHeaderFrom(webResponse);
                    using (var reader = new StreamReader(webResponse.GetResponseStream()))
                    {
                        body = reader.ReadToEnd();
                    }

                }
            }
            catch (WebException exception)
            {
                using (var webResponse = (HttpWebResponse)exception.Response)
                {
                    statusCode = (int)webResponse.StatusCode;
                    headers = ExtractHeaderFrom(webResponse);
                    using (var reader = new StreamReader(webResponse.GetResponseStream()))
                    {
                        body = reader.ReadToEnd();
                    }
                }
            }

            return Create(statusCode, headers, body);
        }

        private static Response Create(
            int statusCode,
            IEnumerable<KeyValuePair<string, string>> headers,
            string body)
        {
            return new Response
                       {
                           StatusCode = statusCode,
                           Headers = headers,
                           Body = body,
                           Success = SuccessfulCodes.Contains(statusCode)
                       };
        }

        private static IEnumerable<KeyValuePair<string, string>> ExtractHeaderFrom(HttpWebResponse webResponse)
        {
            return webResponse
                .Headers
                .AllKeys
                .Select(
                    headerKey =>
                        new KeyValuePair<string, string>(
                            headerKey,
                            webResponse.GetResponseHeader(headerKey))).ToList();
        }
    }
}
