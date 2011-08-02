using System.Collections.Generic;
using Maplink.Webservices.Places.Client.Entities;
using Maplink.Webservices.Places.Client.Wrappers;

namespace Maplink.Webservices.Places.Client.Builders
{
    public class RequestBuilder
    {
        private readonly IUriBuilder _uriBuilder;
        private readonly IClock _clock;
        private readonly ISignatureBuilder _signatureBuilder;
        private readonly IHeaderBuilder _headerBuilder;
        private readonly IAuthorizationBuilder _authorizationBuilder;

        public RequestBuilder(
            IUriBuilder uriBuilder,
            IClock clock,
            ISignatureBuilder signatureBuilder,
            IHeaderBuilder headerBuilder,
            IAuthorizationBuilder authorizationBuilder)
        {
            _uriBuilder = uriBuilder;
            _clock = clock;
            _signatureBuilder = signatureBuilder;
            _headerBuilder = headerBuilder;
            _authorizationBuilder = authorizationBuilder;
        }

        public Request ForRadiusSearch(RadiusSearchRequest radiusRequest)
        {
            var uriBuilt = _uriBuilder.ForRadiusSearch(radiusRequest);

            var requestDateInUtc = _clock.UtcNow();

            var signatureBuilt = 
                _signatureBuilder
                    .For("get", requestDateInUtc, uriBuilt, radiusRequest.Login, radiusRequest.Key);

            var dateHeader = _headerBuilder.ForXMaplinkDate(requestDateInUtc);

            var signatureHeader =
                _headerBuilder
                    .ForAuthorization(
                        _authorizationBuilder.For(radiusRequest.Login, signatureBuilt));

            return new Request
                       {
                           Uri = uriBuilt,
                           Headers = new List<KeyValuePair<string, string>>
                                         {
                                             dateHeader,
                                             signatureHeader
                                         }
                       };
        }
    }
}
