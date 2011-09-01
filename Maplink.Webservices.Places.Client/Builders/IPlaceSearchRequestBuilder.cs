using Maplink.Webservices.Places.Client.Arguments;

namespace Maplink.Webservices.Places.Client.Builders
{
    public interface IPlaceSearchRequestBuilder
    {
        IPlaceSearchRequestBuilder ForLicense(string login, string key);
        IPlaceSearchRequestBuilder BasedOnRadius(double radius, double latitude, double longitude);
        IPlaceSearchRequestBuilder FilteredByTerm(string term);
        IPlaceSearchRequestBuilder FilteredByCategory(int categoryId);
        IPlaceSearchRequestBuilder StartingAtIndex(int startIndex);
        PlaceSearchRequest Build();
    }
}