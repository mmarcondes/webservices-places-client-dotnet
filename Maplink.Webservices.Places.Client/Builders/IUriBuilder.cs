using Maplink.Webservices.Places.Client.Entities;

namespace Maplink.Webservices.Places.Client.Builders
{
    public interface IUriBuilder
    {
        string ForPagination(string uri);
        string For(SearchRequest request);
    }
}