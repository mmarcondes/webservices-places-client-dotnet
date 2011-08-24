using System;
using System.Collections.Generic;

namespace Maplink.Webservices.Places.Client.Builders
{
    public class AllHttpHeadersBuilder : IAllHttpHeadersBuilder
    {
        private readonly IHttpHeaderBuilder _httpHeaderBuilder;
        private readonly IAuthorizationBuilder _authorizationBuilder;

        public AllHttpHeadersBuilder(IHttpHeaderBuilder httpHeaderBuilder, IAuthorizationBuilder authorizationBuilder)
        {
            _httpHeaderBuilder = httpHeaderBuilder;
            _authorizationBuilder = authorizationBuilder;
        }

        public IEnumerable<KeyValuePair<string, string>> For(
            DateTime requestDate,
            string login,
            string signature)
        {
            var dateHeader = _httpHeaderBuilder.ForXMaplinkDate(requestDate);

            var signatureHeader =
                _httpHeaderBuilder
                    .ForAuthorization(
                        _authorizationBuilder.For(login, signature));

            return new List<KeyValuePair<string, string>>
                       {
                           dateHeader,
                           signatureHeader
                       };
        }
    }
}
