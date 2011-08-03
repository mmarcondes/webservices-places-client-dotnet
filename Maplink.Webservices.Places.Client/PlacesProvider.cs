using System.Collections.Generic;
using Maplink.Webservices.Places.Client.Builders;
using Maplink.Webservices.Places.Client.Converters;
using Maplink.Webservices.Places.Client.Entities;
using Maplink.Webservices.Places.Client.Helpers;
using Maplink.Webservices.Places.Client.Services;
using Maplink.Webservices.Places.Client.Wrappers;

namespace Maplink.Webservices.Places.Client
{
    public class PlaceSearcher : IPlaceSearcher
    {
        private readonly IPlacesSearchRetriever _retriever;
        private readonly IPlacesConverter _converter;

        public PlaceSearcher(IPlacesSearchRetriever retriever, IPlacesConverter converter)
        {
            _retriever = retriever;
            _converter = converter;
        }

        public PlaceSearcher()
            : this(
                new PlacesSearchRetriever(
                    new RequestBuilder(
                        new UriBuilder(new ConfigurationWrapper()), 
                        new Clock(), 
                        new SignatureBuilder(new HmacSha1HashGenerator()),
                        new AllHeadersBuilder(new HeaderBuilder(), new AuthorizationBuilder(new Base64Encoder()))),
                    new HttpClient(), 
                    new XmlSerializerWrapper()), 
                new PlacesConverter())
        {
        }

        public IEnumerable<Place> ByRadius(RadiusSearchRequest radiusSearchRequest)
        {
            var places = _retriever.RetrieveFrom(radiusSearchRequest);

            return _converter.ToEntity(places);
        }
    }
}
