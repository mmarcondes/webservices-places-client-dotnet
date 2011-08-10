using System;
using System.Globalization;
using Maplink.Webservices.Places.Client.Entities;
using Maplink.Webservices.Places.Client.Wrappers;

namespace Maplink.Webservices.Places.Client.Builders
{
    public class UriBuilder : IUriBuilder
    {
        private readonly IConfigurationWrapper _configurationWrapper;
        private const string BaseUriKey = "Maplink.Webservices.Places.BaseUri";

        public UriBuilder(IConfigurationWrapper configurationWrapper)
        {
            _configurationWrapper = configurationWrapper;
        }

        public string ForRadiusSearch(RadiusSearchRequest request)
        {
            var culture = CultureInfo.GetCultureInfo("en-us");

            return String.Format(
                "{0}/places/byradius/?radius={1}&latitude={2}&longitude={3}&start={4}",
                    _configurationWrapper.ValueFor(BaseUriKey),
                    request.Radius,
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