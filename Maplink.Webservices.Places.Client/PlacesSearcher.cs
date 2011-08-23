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
        private readonly IPlaceSearchPaginationRequestBuilder _placeSearchPaginationRequestBuilder;
        private static readonly CultureInfo UnitedStatesCultureInfo = CultureInfo.GetCultureInfo("en-us");

        public PlaceSearcher(
            IPlacesSearchRetriever retriever, 
            IPlacesConverter converter, 
            ISearchRequestBuilder searchRequestBuilder,
            IPlaceSearchPaginationRequestBuilder placeSearchPaginationRequestBuilder)
        {
            _retriever = retriever;
            _converter = converter;
            _searchRequestBuilder = searchRequestBuilder;
            _placeSearchPaginationRequestBuilder = placeSearchPaginationRequestBuilder;
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
                new SearchRequestBuilder(),
                new PlaceSearchPaginationRequestBuilder())
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

        public PlaceSearchResult ByTerm(
            LicenseInfo licenseInfo, 
            string term)
        {
            var searchRequest = _searchRequestBuilder
                .WithLicenseInfo(licenseInfo.Login, licenseInfo.Key)
                .WithUriPath("places/byterm")
                .WithStartIndex(0)
                .WithArgument("term", term)
                .Build();

            var places = _retriever.RetrieveFrom(searchRequest);

            return ToEntity(places);
        }

        public PlaceSearchResult ByPaginationUri(LicenseInfo licenseInfo, string paginationUri)
        {
                var placeSearchPaginationRequest = _placeSearchPaginationRequestBuilder
                .WithLicenseInfo(licenseInfo.Login, licenseInfo.Key)
                .WithUri(paginationUri)
                .Build();
                
            var places = _retriever.RetrieveFrom(placeSearchPaginationRequest);

            return ToEntity(places);
        }

        public PlaceSearchResult ByCategory(LicenseInfo licenseInfo, int categoryId)
        {
            var searchRequest = _searchRequestBuilder
                .WithLicenseInfo(licenseInfo.Login, licenseInfo.Key)
                .WithUriPath("places/bycategory")
                .WithArgument("categoryId", categoryId.ToString())
                .WithStartIndex(0)
                .Build();
            var places = _retriever.RetrieveFrom(searchRequest);

            return ToEntity(places);
        }

        private PlaceSearchResult ToEntity(Resources.Places resource)
        {
            return _converter.ToEntity(resource);
        }
    }
}
