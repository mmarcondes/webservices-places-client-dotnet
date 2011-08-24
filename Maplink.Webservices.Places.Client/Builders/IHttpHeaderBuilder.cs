using System;
using System.Collections.Generic;

namespace Maplink.Webservices.Places.Client.Builders
{
    public interface IHttpHeaderBuilder
    {
        KeyValuePair<string, string> ForXMaplinkDate(DateTime date);
        KeyValuePair<string, string> ForAuthorization(string base64UserAndSignature);
    }
}