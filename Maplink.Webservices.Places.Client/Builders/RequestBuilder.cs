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

        public Request ForRadiusSearch(RadiusSearchRequest radiusRequest)
        {
            var uriBuilt = _uriBuilder.ForRadiusSearch(radiusRequest);

            var requestDateInUtc = _clock.UtcNow();

            var signatureBuilt = 
                _signatureBuilder
                    .For("get", requestDateInUtc, uriBuilt, radiusRequest.Login, radiusRequest.Key);

            var headers = _allHeadersBuilder.For(requestDateInUtc, radiusRequest.Login, signatureBuilt);

            return new Request
                       {
                           Uri = uriBuilt,
                           Headers = headers
                       };
        }
    }
}
