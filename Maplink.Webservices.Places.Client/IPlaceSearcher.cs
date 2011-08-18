using Maplink.Webservices.Places.Client.Entities;

namespace Maplink.Webservices.Places.Client
{
    public interface IPlaceSearcher
    {
        PlaceSearchResult ByRadius(
            LicenseInfo licenseInfo,
            double radius,
            double latitude,
            double longitude);
        PlaceSearchResult ByPaginationUri(string paginationUri, LicenseInfo licenseInfo);
    }
}