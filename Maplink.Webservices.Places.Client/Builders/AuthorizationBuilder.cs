using System;
using Maplink.Webservices.Places.Client.Helpers;

namespace Maplink.Webservices.Places.Client.Builders
{
    public class AuthorizationBuilder : IAuthorizationBuilder
    {
        private readonly IBase64Encoder _base64Encoder;

        public AuthorizationBuilder(IBase64Encoder base64Encoder)
        {
            _base64Encoder = base64Encoder;
        }

        public string For(string login, string signature)
        {
            var contentToBeEncoded =
                String.Format(
                    "{0}:{1}",
                    login,
                    signature);

            return _base64Encoder.Encode(contentToBeEncoded);
        }
    }
}
