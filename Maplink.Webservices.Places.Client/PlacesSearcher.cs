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

        public PlaceSearchResult ByRadius(
            LicenseInfo licenseInfo, 
            double radius, 
            double latitude, 
            double longitude)
        {
            var searchRequest = _requestBuilder
                .WithLicenseInfo(licenseInfo.Login, licenseInfo.Key)
                .WithUriPath("/places/byradius")
                .WithStartIndex(0)
                .WithArgument("radius", radius.ToString(UnitedStatesCultureInfo))
                .WithArgument("latitude", latitude.ToString(UnitedStatesCultureInfo))
                .WithArgument("longitude", longitude.ToString(UnitedStatesCultureInfo))
                .Build();

            return RetrievePlaces(searchRequest);
        }

        public PlaceSearchResult ByTerm(
            LicenseInfo licenseInfo, 
            string term)
        {
            var searchRequest = _requestBuilder
                .WithLicenseInfo(licenseInfo.Login, licenseInfo.Key)
                .WithUriPath("/places/byterm")
                .WithStartIndex(0)
                .WithArgument("term", term)
                .Build();

            return RetrievePlaces(searchRequest);
        }

        public PlaceSearchResult ByCategory(LicenseInfo licenseInfo, int categoryId)
        {
            var searchRequest = _requestBuilder
                .WithLicenseInfo(licenseInfo.Login, licenseInfo.Key)
                .WithUriPath("/places/bycategory")
                .WithArgument("categoryId", categoryId.ToString())
                .WithStartIndex(0)
                .Build();

            return RetrievePlaces(searchRequest);
        }

        public PlaceSearchResult ByUri(LicenseInfo licenseInfo, string uriPathAndQuery)
        {
            var searchRequest = _requestBuilder
                .WithLicenseInfo(licenseInfo.Login, licenseInfo.Key)
                .WithUriPathAndQuery(uriPathAndQuery)
                .Build();

            return RetrievePlaces(searchRequest);
        }

        private PlaceSearchResult RetrievePlaces(Request searchRequest)
        {
            var places = _retriever.From<Resources.Places>(searchRequest);

            return _converter.ToEntity(places);
        }
    }
}
