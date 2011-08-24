using Maplink.Webservices.Places.Client.Entities;
using Maplink.Webservices.Places.Client.Wrappers;

namespace Maplink.Webservices.Places.Client.Builders
{
    public interface IHttpRequestBuilder
    {
        HttpRequest For(SearchRequest searchRequest);
        HttpRequest ForCustomRequest(CustomRequest customRequest);
    }
}