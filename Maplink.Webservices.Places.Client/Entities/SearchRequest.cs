using System.Collections.Generic;

namespace Maplink.Webservices.Places.Client.Entities
{
    public class SearchRequest
    {
        public string UriPath { get; set; }

        public IEnumerable<KeyValuePair<string, string>> Arguments { get; set; }
    }
}