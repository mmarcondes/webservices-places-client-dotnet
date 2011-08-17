using System.Collections.Generic;

namespace Maplink.Webservices.Places.Client.Builders
{
    public interface IUriQueryBuilder
    {
        string Build(IEnumerable<KeyValuePair<string, string>> arguments);
    }
}