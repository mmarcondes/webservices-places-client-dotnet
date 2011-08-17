using Maplink.Webservices.Places.Client.Entities;
using Maplink.Webservices.Places.Client.Wrappers;

namespace Maplink.Webservices.Places.Client.Builders
{
    public interface IRequestBuilder
    {
        Request For(SearchRequest searchRequest);
        Request ForRadiusSearch(PlaceSearchPaginationRequest placeSearchPaginationRequest);
    }
}