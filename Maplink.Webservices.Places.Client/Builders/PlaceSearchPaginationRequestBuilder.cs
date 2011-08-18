using Maplink.Webservices.Places.Client.Entities;

namespace Maplink.Webservices.Places.Client.Builders
{
    public class PlaceSearchPaginationRequestBuilder : IPlaceSearchPaginationRequestBuilder
    {
        private readonly PlaceSearchPaginationRequest _placeSearchPaginationRequest; 

        public PlaceSearchPaginationRequestBuilder()
        {
            _placeSearchPaginationRequest = new PlaceSearchPaginationRequest();
        }

        public IPlaceSearchPaginationRequestBuilder WithLicenseInfo(string licenseLogin, string licenseKey)
        {
            _placeSearchPaginationRequest.Login = licenseLogin;
            _placeSearchPaginationRequest.Key = licenseKey;

            return this;
        }

        public IPlaceSearchPaginationRequestBuilder WithUri(string uri)
        {
            _placeSearchPaginationRequest.Uri = uri;

            return this;
        }

        public PlaceSearchPaginationRequest Build()
        {
            return _placeSearchPaginationRequest;
        }
    }
}
