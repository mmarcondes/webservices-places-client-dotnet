using System.Collections.Generic;
using Maplink.Webservices.Places.Client.Builders;
using Maplink.Webservices.Places.Client.Converters;
using Maplink.Webservices.Places.Client.Entities;
using Maplink.Webservices.Places.Client.Helpers;
using Maplink.Webservices.Places.Client.Wrappers;

namespace Maplink.Webservices.Places.Client
{
    public class PlacesProvider
    {
        private readonly IRequestBuilder _requestBuilder;
        private readonly IHttpClient _httpClient;
        private readonly IXmlSerializerWrapper _serializerWrapper;
        private readonly IPlacesConverter _converter;

        public PlacesProvider(
            IRequestBuilder requestBuilder,
            IHttpClient httpClient,
            IXmlSerializerWrapper serializerWrapper,
            IPlacesConverter converter)
        {
            _requestBuilder = requestBuilder;
            _httpClient = httpClient;
            _serializerWrapper = serializerWrapper;
            _converter = converter;
        }

        public PlacesProvider()
            : this(
                new RequestBuilder(
                    new UriBuilder(new ConfigurationWrapper()), 
                    new Clock(), 
                    new SignatureBuilder(new HmacSha1HashGenerator()),
                    new AllHeadersBuilder(new HeaderBuilder(), new AuthorizationBuilder(new Base64Encoder()))),
                new HttpClient(), 
                new XmlSerializerWrapper(), 
                new PlacesConverter())
        {
            
        }

        public IEnumerable<Place> ByRadius(RadiusSearchRequest radiusSearchRequest)
        {
            var request = _requestBuilder.ForRadiusSearch(radiusSearchRequest);

            var response = _httpClient.Get(request);

            var places = _serializerWrapper.Deserialize<Resources.Places>(response.Body);

            return _converter.ToEntity(places);
        }
    }
}
