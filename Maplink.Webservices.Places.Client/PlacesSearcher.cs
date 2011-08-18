using System.Globalization;
using Maplink.Webservices.Places.Client.Builders;
using Maplink.Webservices.Places.Client.Converters;
using Maplink.Webservices.Places.Client.Entities;
using Maplink.Webservices.Places.Client.Helpers;
using Maplink.Webservices.Places.Client.Services;
using Maplink.Webservices.Places.Client.Wrappers;
using UriBuilder = Maplink.Webservices.Places.Client.Builders.UriBuilder;

namespace Maplink.Webservices.Places.Client
{
    public class PlaceSearcher : IPlaceSearcher
    {
        private readonly IPlacesSearchRetriever _retriever;
        private readonly IPlacesConverter _converter;
        private readonly ISearchRequestBuilder _searchRequestBuilder;
        private static readonly CultureInfo UnitedStatesCultureInfo = CultureInfo.GetCultureInfo("en-us");

        public PlaceSearcher(
            IPlacesSearchRetriever retriever, 
            IPlacesConverter converter, 
            ISearchRequestBuilder searchRequestBuilder)
        {
            _retriever = retriever;
            _converter = converter;
            _searchRequestBuilder = searchRequestBuilder;
        }

        public PlaceSearcher()
            : this(
                new PlacesSearchRetriever(
                    new RequestBuilder(
                        new UriBuilder(new ConfigurationWrapper(), new UriQueryBuilder()), 
                        new Clock(), 
                        new SignatureBuilder(new HmacSha1HashGenerator()),
                        new AllHeadersBuilder(new HeaderBuilder(), new AuthorizationBuilder(new Base64Encoder()))),
                    new HttpClient(), 
                    new XmlSerializerWrapper()), 
                new PlacesConverter(), 
                new SearchRequestBuilder())
        {
        }

        public PlaceSearchResult ByRadius(
            LicenseInfo licenseInfo, 
            double radius, 
            double latitude, 
            double longitude)
        {
            var searchRequest = _searchRequestBuilder
                .WithLicenseInfo(licenseInfo.Login, licenseInfo.Key)
                .WithUriPath("places/byradius")
                .WithStartIndex(0)
                .WithArgument("radius", radius.ToString(UnitedStatesCultureInfo))
                .WithArgument("latitude", latitude.ToString(UnitedStatesCultureInfo))
                .WithArgument("longitude", longitude.ToString(UnitedStatesCultureInfo))
                .Build();

            var places = _retriever.RetrieveFrom(searchRequest);

            return ToEntity(places);
        }

        public PlaceSearchResult ByRadius(PlaceSearchPaginationRequest placeSearchPaginationRequest)
        {
            var places = _retriever.RetrieveFrom(placeSearchPaginationRequest);

            return ToEntity(places);
        }

        private PlaceSearchResult ToEntity(Resources.Places resource)
        {
            return _converter.ToEntity(resource);
        }
    }
}
