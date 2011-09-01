using System.Globalization;
using Maplink.Webservices.Places.Client.Arguments;
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
        private readonly IResourceRetriever _retriever;
        private readonly IPlacesConverter _converter;
        private readonly IRequestBuilder _requestBuilder;
        private static readonly CultureInfo UnitedStatesCultureInfo = CultureInfo.GetCultureInfo("en-us");

        public PlaceSearcher(
            IResourceRetriever retriever, 
            IPlacesConverter converter, 
            IRequestBuilder requestBuilder)
        {
            _retriever = retriever;
            _converter = converter;
            _requestBuilder = requestBuilder;
        }

        public PlaceSearcher()
            : this(
                new ResourceRetriever(
                    new HttpRequestBuilder(
                        new UriBuilder(new ConfigurationWrapper(), new UriQueryBuilder()), 
                        new Clock(), 
                        new SignatureBuilder(new HmacSha1HashGenerator()),
                        new AllHttpHeadersBuilder(new HttpHeaderBuilder(), new AuthorizationBuilder(new Base64Encoder()))),
                    new HttpClient(), 
                    new XmlSerializerWrapper()), 
                new PlacesConverter(), 
                new RequestBuilder())
        {
        }

        public PlaceSearchResult ByRadius(PlaceSearchRequest placeSearchRequest)
        {
            var searchRequest = _requestBuilder
                .WithUriPath("/places/byradius")
                .WithLicenseInfo(placeSearchRequest.LicenseInfo)
                .WithStartIndex(placeSearchRequest.StartIndex)
                .WithArgument("radius", placeSearchRequest.Radius.ToString(UnitedStatesCultureInfo))
                .WithArgument("latitude", placeSearchRequest.Latitude.ToString(UnitedStatesCultureInfo))
                .WithArgument("longitude", placeSearchRequest.Longitude.ToString(UnitedStatesCultureInfo))
                .WithArgument("term", placeSearchRequest.Term)
                .WithArgument("category", placeSearchRequest.CategoryId.ToString())
                .Build();

            return RetrievePlacesFor(searchRequest);
        }

        public PlaceSearchResult ByUri(PaginationRequest paginationRequest)
        {
            var searchRequest = _requestBuilder
                .WithLicenseInfo(paginationRequest.LicenseInfo)
                .WithUriPathAndQuery(paginationRequest.UriPathAndQuery)
                .Build();

            return RetrievePlacesFor(searchRequest);
        }

        private PlaceSearchResult RetrievePlacesFor(Request searchRequest)
        {
            var places = _retriever.From<Resources.Places>(searchRequest);

            return _converter.ToEntity(places);
        }
    }
}
