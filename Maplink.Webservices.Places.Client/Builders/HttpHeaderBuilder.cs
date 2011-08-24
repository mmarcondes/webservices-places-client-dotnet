using System;
using System.Collections.Generic;

namespace Maplink.Webservices.Places.Client.Builders
{
    public class HttpHeaderBuilder : IHttpHeaderBuilder
    {
        public KeyValuePair<string, string> ForXMaplinkDate(DateTime date)
        {
            return new KeyValuePair<string, string>(
                "X-Maplink-Date", 
                date.ToString("r"));
        }

        public KeyValuePair<string, string> ForAuthorization(string base64UserAndSignature)
        {
            return new KeyValuePair<string, string>(
                "Authorization", 
                String.Format("MAPLINKWS {0}", base64UserAndSignature));
        }
    }
}
