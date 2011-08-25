using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Maplink.Webservices.Places.Client.Builders
{
    public class UriQueryBuilder : IUriQueryBuilder
    {
        public string Build(IEnumerable<KeyValuePair<string, string>> arguments)
        {
            var queryStrings = 
                arguments
                    .Select(
                        argument => 
                            String.Format(
                                "{0}={1}", 
                                HttpUtility.UrlEncode(argument.Key), 
                                HttpUtility.UrlEncode(argument.Value)));

            return String.Join("&", queryStrings.ToArray());
        }
    }
}