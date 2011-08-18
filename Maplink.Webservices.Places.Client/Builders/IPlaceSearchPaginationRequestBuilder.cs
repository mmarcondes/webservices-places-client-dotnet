using Maplink.Webservices.Places.Client.Entities;

namespace Maplink.Webservices.Places.Client.Builders
{
    public interface IPlaceSearchPaginationRequestBuilder
    {
        IPlaceSearchPaginationRequestBuilder WithLicenseInfo(string licenseLogin, string licenseKey);
        IPlaceSearchPaginationRequestBuilder WithUri(string uri);
        PlaceSearchPaginationRequest Build();
    }
}