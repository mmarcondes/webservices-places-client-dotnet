using System;

namespace Maplink.Webservices.Places.Client.Builders
{
    public interface ISignatureBuilder
    {
        string For(
            string requestHttpVerb,
            DateTime requestDate,
            string requestUri,
            string login,
            string key);
    }
}