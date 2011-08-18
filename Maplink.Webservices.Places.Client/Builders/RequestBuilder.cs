using Maplink.Webservices.Places.Client.Entities;
using Maplink.Webservices.Places.Client.Wrappers;

namespace Maplink.Webservices.Places.Client.Builders
{
    public class RequestBuilder : IRequestBuilder
    {
        private readonly IUriBuilder _uriBuilder;
        private readonly IClock _clock;
        private readonly ISignatureBuilder _signatureBuilder;
        private readonly IAllHeadersBuilder _allHeadersBuilder;

        public RequestBuilder(
            IUriBuilder uriBuilder,
            IClock clock,
            ISignatureBuilder signatureBuilder,
            IAllHeadersBuilder allHeadersBuilder)
        {
            _uriBuilder = uriBuilder;
            _clock = clock;
            _signatureBuilder = signatureBuilder;
            _allHeadersBuilder = allHeadersBuilder;
        }

        public Request For(SearchRequest searchRequest)
        {
            var uriBuilt = _uriBuilder.For(searchRequest);
            return CreateHttpRequest(searchRequest.LicenseLogin, searchRequest.LicenseKey, uriBuilt);
        }

        public Request ForPaginationSearch(PlaceSearchPaginationRequest placeSearchPaginationRequest)
        {
            var uriBuilt = _uriBuilder.ForPagination(placeSearchPaginationRequest.Uri);

            return CreateHttpRequest(
                placeSearchPaginationRequest.Login, 
                placeSearchPaginationRequest.Key,
                uriBuilt);
        }

        private Request CreateHttpRequest(string login, string key, string uriBuilt)
        {
            var requestDateInUtc = _clock.UtcHourNow();

            var signatureBuilt =
                _signatureBuilder
                    .For("get", requestDateInUtc, uriBuilt, login, key);

            var headers = _allHeadersBuilder.For(requestDateInUtc, login, signatureBuilt);

            return new Request
                       {
                           Uri = uriBuilt,
                           Headers = headers
                       };
        }
    }
}
