using System.Collections.Generic;

namespace Maplink.Webservices.Places.Client.Wrappers
{
    public class HttpResponse
    {
        public int StatusCode { get; set; }
        public IEnumerable<KeyValuePair<string, string>> Headers { get; set; }
        public string Body { get; set; }
        public bool Success { get; set; }
    }
}
