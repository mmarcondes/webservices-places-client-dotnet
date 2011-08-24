using Maplink.Webservices.Places.Client.Entities;

namespace Maplink.Webservices.Places.Client.Builders
{
    public class CustomRequestBuilder : ICustomRequestBuilder
    {
        private readonly CustomRequest _customRequest; 

        public CustomRequestBuilder()
        {
            _customRequest = new CustomRequest();
        }

        public ICustomRequestBuilder WithLicenseInfo(string licenseLogin, string licenseKey)
        {
            _customRequest.Login = licenseLogin;
            _customRequest.Key = licenseKey;

            return this;
        }

        public ICustomRequestBuilder WithUri(string uri)
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
