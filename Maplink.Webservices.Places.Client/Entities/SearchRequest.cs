using System;
using System.Collections.Generic;

namespace Maplink.Webservices.Places.Client.Entities
{
    public class SearchRequest
    {
        public SearchRequest()
        {
            LicenseLogin = String.Empty;
            LicenseKey = String.Empty;
        }

        public string UriPath { get; set; }
        public IEnumerable<KeyValuePair<string, string>> Arguments { get; set; }
        public int StartsAtIndex { get; set; }
        public string LicenseLogin { get; set; }
        public string LicenseKey { get; set; }
    }
}