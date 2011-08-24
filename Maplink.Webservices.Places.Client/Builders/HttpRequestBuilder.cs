using Maplink.Webservices.Places.Client.Entities;
using Maplink.Webservices.Places.Client.Wrappers;

namespace Maplink.Webservices.Places.Client.Builders
{
    public class HttpRequestBuilder : IHttpRequestBuilder
    {
        private readonly IUriBuilder _uriBuilder;
        private readonly IClock _clock;
        private readonly ISignatureBuilder _signatureBuilder;
        private readonly IAllHttpHeadersBuilder _allHttpHeadersBuilder;

        public HttpRequestBuilder(
            IUriBuilder uriBuilder,
            IClock clock,
            ISignatureBuilder signatureBuilder,
            IAllHttpHeadersBuilder allHttpHeadersBuilder)
        {
            _uriBuilder = uriBuilder;
            _clock = clock;
            _signatureBuilder = signatureBuilder;
            _allHttpHeadersBuilder = allHttpHeadersBuilder;
        }

        public HttpRequest For(Request request)
        {
            var uriBuilt = _uriBuilder.For(request);
            return CreateHttpRequest(request.LicenseLogin, request.LicenseKey, uriBuilt);
        }

        private HttpRequest CreateHttpRequest(string login, string key, string uriBuilt)
        {
            var requestDateInUtc = _clock.UtcHourNow();

            var signatureBuilt =
                _signatureBuilder
                    .For("get", requestDateInUtc, uriBuilt, login, key);

            var headers = _allHttpHeadersBuilder.For(requestDateInUtc, login, signatureBuilt);

            return new HttpRequest
                       {
                           Uri = uriBuilt,
                           Headers = headers
                       };
        }
    }
}
