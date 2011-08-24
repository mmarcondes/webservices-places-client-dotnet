using Maplink.Webservices.Places.Client.Entities;

namespace Maplink.Webservices.Places.Client.Builders
{
    public class PlaceSearchPaginationRequestBuilder : IPlaceSearchPaginationRequestBuilder
    {
        private readonly CustomRequest _customRequest; 

        public PlaceSearchPaginationRequestBuilder()
        {
            _customRequest = new CustomRequest();
        }

        public IPlaceSearchPaginationRequestBuilder WithLicenseInfo(string licenseLogin, string licenseKey)
        {
            _customRequest.Login = licenseLogin;
            _customRequest.Key = licenseKey;

            return this;
        }

        public IPlaceSearchPaginationRequestBuilder WithUri(string uri)
        {
            _customRequest.UriPathAndQuery = uri;

            return this;
        }

        public CustomRequest Build()
        {
            return _customRequest;
        }
    }
}
