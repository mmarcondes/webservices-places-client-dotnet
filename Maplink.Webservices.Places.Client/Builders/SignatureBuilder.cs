using System;
using Maplink.Webservices.Places.Client.Helpers;

namespace Maplink.Webservices.Places.Client.Builders
{
    public class SignatureBuilder : ISignatureBuilder
    {
        private readonly IHmacSha1HashGenerator _hashGenerator;

        public SignatureBuilder(IHmacSha1HashGenerator hashGenerator)
        {
            _hashGenerator = hashGenerator;
        }

        public string For(
            string requestHttpVerb,
            DateTime requestDate,
            string requestUri,
            string login,
            string key)
        {
            var signatureContent =
                String.Format(
                "{0}\n{1}\n{2}\n{3}",
                requestHttpVerb.ToUpper(),
                requestDate.ToString("r"),
                requestUri,
                login);

            return _hashGenerator.For(signatureContent, key);
        }
    }
}
