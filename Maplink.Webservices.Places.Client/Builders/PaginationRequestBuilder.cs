using Maplink.Webservices.Places.Client.Arguments;
using Maplink.Webservices.Places.Client.Entities;

namespace Maplink.Webservices.Places.Client.Builders
{
    public class PaginationRequestBuilder : IPaginationRequestBuilder
    {
        private PaginationRequest _request;

        public PaginationRequestBuilder()
        {
            _request = CreateDefaultPaginationRequest();
        }

        public IPaginationRequestBuilder ForLicense(string login, string key)
        {
            _request.LicenseInfo.Login = login;
            _request.LicenseInfo.Key = key;

            return this;
        }

        public IPaginationRequestBuilder WithUriPathAndQuery(string uriPathAndQuery)
        {
            _request.UriPathAndQuery = uriPathAndQuery;

            return this;
        }

        public PaginationRequest Build()
        {
            var request = _request;

            _request = CreateDefaultPaginationRequest();

            return request;
        }

        private static PaginationRequest CreateDefaultPaginationRequest()
        {
            return new PaginationRequest
                       {
                           LicenseInfo = new LicenseInfo()
                       };
        }
    }
}