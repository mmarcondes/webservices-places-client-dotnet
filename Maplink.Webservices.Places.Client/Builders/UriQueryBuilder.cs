using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Maplink.Webservices.Places.Client.Builders
{
    public class UriQueryBuilder : IUriQueryBuilder
    {
        public string Build(IEnumerable<KeyValuePair<string, string>> arguments)
        {
            var uriQuery = arguments
                .Aggregate(
                    String.Empty, 
                    (current, argument) =>
                        current + String.Format("{0}={1}&", 
                        HttpUtility.UrlEncode(argument.Key), 
                        HttpUtility.UrlEncode(argument.Value)));

            return new Regex(@"&$").Replace(uriQuery, String.Empty);
        }
    }
}