using System;
using System.Collections.Generic;

namespace Maplink.Webservices.Places.Client.Builders
{
    public interface IAllHttpHeadersBuilder
    {
        IEnumerable<KeyValuePair<string, string>> For(
            DateTime requestDate,
            string login,
            string signature);
    }
}