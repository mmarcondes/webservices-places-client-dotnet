using Maplink.Webservices.Places.Client.Entities;

namespace Maplink.Webservices.Places.Client
{
    public interface IPlaceSearcher
    {
        PlaceSearchResult ByRadius(SearchRequest searchRequest);
        PlaceSearchResult ByRadius(PlaceSearchPaginationRequest placeSearchPaginationRequest);
    }
}