using System;
using System.Collections.Generic;

namespace Maplink.Webservices.Places.Client.Entities
{
    public class SearchRequest
    {
        public SearchRequest()
        {
            Login = String.Empty;
            Key = String.Empty;
        }

        public string UriPath { get; set; }
        public IEnumerable<KeyValuePair<string, string>> Arguments { get; set; }
        public int StartsAtIndex { get; set; }
        public string Login { get; set; }
        public string Key { get; set; }
    }
}