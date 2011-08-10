using Maplink.Webservices.Places.Client.Entities;

namespace Maplink.Webservices.Places.Client
{
    public interface IPlaceSearcher
    {
        PlaceSearchResult ByRadius(RadiusSearchRequest radiusSearchRequest);
        PlaceSearchResult ByRadius(PlaceSearchPaginationRequest placeSearchPaginationRequest);
    }
}