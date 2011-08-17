using Maplink.Webservices.Places.Client.Entities;

namespace Maplink.Webservices.Places.Client.Services
{
    public interface IPlacesSearchRetriever
    {
        Resources.Places RetrieveFrom(SearchRequest searchRequest);
        Resources.Places RetrieveFrom(PlaceSearchPaginationRequest placeSearchPaginationRequest);
    }
}