using Maplink.Webservices.Places.Client.Entities;
using Maplink.Webservices.Places.Client.Wrappers;

namespace Maplink.Webservices.Places.Client.Builders
{
    public interface IHttpRequestBuilder
    {
        HttpRequest For(Request request);
        HttpRequest ForCustomRequest(CustomRequest customRequest);
    }
}