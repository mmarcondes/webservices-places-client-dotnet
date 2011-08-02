using System;
using System.Collections.Generic;

namespace Maplink.Webservices.Places.Client.Builders
{
    public class AllHeadersBuilder : IAllHeadersBuilder
    {
        private readonly IHeaderBuilder _headerBuilder;
        private readonly IAuthorizationBuilder _authorizationBuilder;

        public AllHeadersBuilder(IHeaderBuilder headerBuilder, IAuthorizationBuilder authorizationBuilder)
        {
            _headerBuilder = headerBuilder;
            _authorizationBuilder = authorizationBuilder;
        }

        public IEnumerable<KeyValuePair<string, string>> For(
            DateTime requestDate,
            string login,
            string signature)
        {
            var dateHeader = _headerBuilder.ForXMaplinkDate(requestDate);

            var signatureHeader =
                _headerBuilder
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
