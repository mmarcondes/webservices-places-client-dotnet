using Maplink.Webservices.Places.Client.Entities;

namespace Maplink.Webservices.Places.Client
{
    public interface IPlaceSearcher
    {
        PlaceSearchResult ByRadius(
            LicenseInfo licenseInfo,
            double radius,
            double latitude,
            double longitude,
            string term = "");
        PlaceSearchResult ByUri(LicenseInfo licenseInfo, string uriPathAndQuery);
        PlaceSearchResult ByCategory(LicenseInfo licenseInfo, int categoryId);
    }
}