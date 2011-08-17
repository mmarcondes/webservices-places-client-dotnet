using System;
using System.Globalization;
using Maplink.Webservices.Places.Client.Entities;
using Maplink.Webservices.Places.Client.Wrappers;

namespace Maplink.Webservices.Places.Client.Builders
{
    public class UriBuilder : IUriBuilder
    {
        private readonly IConfigurationWrapper _configurationWrapper;
        private readonly IUriQueryBuilder _uriQueryBuilder;
        private const string BaseUriKey = "Maplink.Webservices.Places.BaseUri";

        public UriBuilder(IConfigurationWrapper configurationWrapper, IUriQueryBuilder uriQueryBuilder)
        {
            _configurationWrapper = configurationWrapper;
            _uriQueryBuilder = uriQueryBuilder;
        }

        public string For(SearchRequest request)
        {
            var uriQuery = _uriQueryBuilder.Build(request.Arguments);

            return String.Format(
                "{0}/{1}?{2}", 
                _configurationWrapper.ValueFor(BaseUriKey), 
                request.UriPath, 
                uriQuery);
        }

        public string ForRadiusSearch(RadiusSearchRequest request)
        {
            var culture = CultureInfo.GetCultureInfo("en-us");

            return String.Format(
                "{0}/places/byradius/?radius={1}&latitude={2}&longitude={3}&start={4}",
                    _configurationWrapper.ValueFor(BaseUriKey),
                    request.Radius.ToString(culture),
                    request.Latitude.ToString(culture),
                    request.Longitude.ToString(culture),
                    request.StartsAtIndex);
        }

        public string ForPagination(string uri)
        {
            return String.Format(
                "{0}{1}",
                _configurationWrapper.ValueFor(BaseUriKey),
                uri);
        }
    }
}