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
        PlaceSearchResult ByTerm(LicenseInfo licenseInfo, string term);
        PlaceSearchResult ByPaginationUri(LicenseInfo licenseInfo, string paginationUri);
    }
}