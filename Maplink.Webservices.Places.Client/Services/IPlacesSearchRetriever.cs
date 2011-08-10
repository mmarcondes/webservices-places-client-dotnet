using Maplink.Webservices.Places.Client.Entities;

namespace Maplink.Webservices.Places.Client.Services
{
    public interface IPlacesSearchRetriever
    {
        Resources.Places RetrieveFrom(RadiusSearchRequest radiusSearchRequest);
        Resources.Places RetrieveFrom(PlaceSearchPaginationRequest placeSearchPaginationRequest);
    }
}